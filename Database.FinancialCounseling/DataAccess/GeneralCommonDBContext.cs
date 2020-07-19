using Database.Common;
using Database.Entities;
using Microsoft.EntityFrameworkCore;


namespace Database.DataAccess
{
    public partial class GeneralCommonDBContext : DbContext, IDbContextCore
    {
        /// <summary>
        /// instantiate the context
        /// </summary>
        /// <param name="options"></param>
        public GeneralCommonDBContext(DbContextOptions<GeneralCommonDBContext> options)
    : base(options)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        public GeneralCommonDBContext()
        {

        }

        /// <summary>
        /// configure context using model builder 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {

        }
        public virtual DbSet<TenantInformation> TenantInformation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TenantInformation>(entity =>
            {
                entity.HasKey(e => e.TenantId);

                entity.Property(e => e.DataSource)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.InitialCatalog)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.TenantName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
