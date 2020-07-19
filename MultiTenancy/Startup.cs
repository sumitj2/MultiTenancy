
using Database.Abstraction.Common;
using Database.Abstraction.Contract.Repository;
using Database.Abstraction.Contract.UnitOfWork;
using Database.Common;
using Database.DataAccess;
using Database.DataAccess.Repository;
using Database.DataAccess.UnitOfWork;
using Database.Entities;
using Database.Multitenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region commented
            //EntityFrameworkConfiguration.ConfigureService(services, Configuration);

            //// services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            ////services.AddHttpContextAccessor();
            //services.Configure<ConnectionSettings>(Configuration.GetSection(DefaultConstants.ConnectionStrings));

            //services.AddDbContext<IDbContextBase, GeneralCommonDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GeneralCommonDataDBConnectionString")));
            //services.AddDbContext<GeneralCommonDBContext>(item => item.UseSqlServer(Configuration.GetConnectionString("GeneralCommonDataDBConnectionString")));

            //services.AddTransient(typeof(IRepositoryBase<TenantInformation>), typeof(TenantInformationRepository));//services.AddTransient<GeneralCommonRepositoryBase<TenantInformation>, TenantInformationRepository>();
            //services.AddTransient<ITenantInformationRepository, TenantInformationRepository>();

            //services.AddScoped(typeof(IUnitOfWorkBase), typeof(GeneralCommonUnitOfWork));
            //services.AddTransient<ITenantInformationUnitOfWork, TenantInformationUnitOfWork>();

            ///**************************/
            ////services.AddScoped(typeof(IUnitOfWorkBase), typeof(TransactionUnitOfWork));
            //services.AddScoped<IContextFactory, ContextFactory>();

            //services.AddScoped(typeof(ITransactionRepository<UserInfo>), typeof(AddUsersInfoInTenants));//services.AddTransient<GeneralCommonRepositoryBase<TenantInformation>, TenantInformationRepository>();
            //services.AddTransient<IAddUsersInfoInTenants, AddUsersInfoInTenants>();

            //services.AddScoped(typeof(IUnitOfWorkBase), typeof(GeneralCommonUnitOfWork));
            //services.AddTransient<IAddUserInfoInTenantsUnitOfWork, AddUserInfoInTenantsUnitOfWork>(); 
            #endregion

            EntityFrameworkConfiguration.ConfigureService(services, Configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<ConnectionSettings>(Configuration.GetSection(DefaultConstants.ConnectionStrings));
            services.AddDbContext<IDbContextCore, GeneralCommonDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GeneralCommonDataDBConnectionString")));

            services.AddScoped<IContextFactory, ContextFactory>();


            services.AddScoped(typeof(ITranUnitOfWork), typeof(TranUnitOfWork));



            services.AddTransient(typeof(IRepository<TenantInformation>), typeof(TenantInfoReposiory));

           
            services.AddScoped(typeof(ITranRepository<UserInfo>), typeof(UserInfoRepository));

            services.AddTransient<ITenantInfoReposiory, TenantInfoReposiory>();
            services.AddScoped<IuserInfoRepository, UserInfoRepository>();

            services.AddScoped<ITenantInfoUnitOfWork, TenantInfoUnitOfWork>();
            services.AddTransient<IUserInfoUnitOfWork, UserInfoUnitOfWork>();


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
            else
            {
                app.UseHsts();
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
