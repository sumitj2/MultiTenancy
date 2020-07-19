using System;
using Abstraction.CommonInterfaces;
using Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Implementation.DataAccessImplementaion
{
    //    public partial class GeneralCommonDBContext : DbContext ,IDbContextBase
    //    {

    //        public GeneralCommonDBContext()
    //        {
    //        }

    //        public GeneralCommonDBContext(DbContextOptions<GeneralCommonDBContext> options)
    //            : base(options)
    //        {
    //        }

    //        public DbContext dbContext { get; set; }


    //        public virtual DbSet<TenantInformation> TenantInformation { get; set; }

    //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //        {
    //            if (!optionsBuilder.IsConfigured)
    //            {
    ////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
    ////                optionsBuilder.UseSqlServer("Data Source=LAPTOP-6IH46700\\SQLEXPRESS;Initial Catalog=GeneralCommonDB;Integrated Security=True");
    //            }
    //        }

    //        protected override void OnModelCreating(ModelBuilder modelBuilder)
    //        {
    //            modelBuilder.Entity<TenantInformation>(entity =>
    //            {
    //                entity.HasKey(x => x.TenantId);

    //                entity.Property(e => e.DataSource)
    //                    .IsRequired()
    //                    .HasMaxLength(50);

    //                entity.Property(e => e.InitialCatalog)
    //                    .IsRequired()
    //                    .HasMaxLength(50);

    //                entity.Property(e => e.Password).HasMaxLength(50);

    //                entity.Property(e => e.TenantName)
    //                    .IsRequired()
    //                    .HasMaxLength(50);

    //                entity.Property(e => e.UserId).HasMaxLength(50);
    //            });

    //           // OnModelCreatingPartial(modelBuilder);
    //        }

    //       // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    //    }

    public partial class GeneralCommonDBContext : DbContext, IDbContextBase
    {
        public GeneralCommonDBContext()
        {
        }

        public GeneralCommonDBContext(DbContextOptions<GeneralCommonDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TenantInformation> TenantInformation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=LAPTOP-6IH46700\\SQLEXPRESS;Initial Catalog=GeneralCommonDB;Integrated Security=True");
            }
        }

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
