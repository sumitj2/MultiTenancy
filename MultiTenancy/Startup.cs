using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstraction.CommonInterfaces;
using Abstraction.DataAccessInterfaces.RepositoryInterfaces;
using Abstraction.DataAccessInterfaces.UnitOfWorkInterfaces;
using Enitities;
using Implementation.Common;
using Implementation.DataAccessImplementaion;
using Implementation.DataAccessImplementaion.RepositoryImplementation;
using Implementation.DataAccessImplementaion.UnitOfWorkImplementaion;
using Implementation.MultiTenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;//FOR SWAGGER    
using IWebHostEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;


namespace MultiTenancy
{
    public class Startup
    {
        public Startup(/*IConfiguration configuration*/IWebHostEnvironment env)
        {
            // Configuration = configuration;
            if (env != null)
            {
                var builder = new ConfigurationBuilder()
                   .SetBasePath(env.ContentRootPath)
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                   .AddEnvironmentVariables();
                Configuration = builder.Build();

                Environment = env;
            }
        }

        //  public IConfiguration Configuration { get; }
        public IConfigurationRoot Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            EntityFrameworkConfiguration.ConfigureService(services, Configuration);
            services.AddDbContext<IDbContextBase, GeneralCommonDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GeneralCommonDataDBConnectionString")));
            services.AddDbContext<GeneralCommonDBContext>(item => item.UseSqlServer(Configuration.GetConnectionString("GeneralCommonDataDBConnectionString")));
           
            services.AddTransient(typeof(IRepositoryBase<TenantInformation>), typeof(TenantInformationRepository));//services.AddTransient<GeneralCommonRepositoryBase<TenantInformation>, TenantInformationRepository>();
            services.AddTransient<ITenantInformationRepository, TenantInformationRepository>();
            
            services.AddScoped(typeof(IUnitOfWorkBase),typeof(GeneralCommonUnitOfWork));
            services.AddTransient<ITenantInformationUnitOfWork, TenantInformationUnitOfWork>();

            services.AddControllers();

            //added for swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Multi tenancy", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Multi tenancy");
            }); 
            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
