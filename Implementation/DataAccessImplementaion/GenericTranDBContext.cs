using Abstraction.CommonInterfaces;
using Enitities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.DataAccessImplementaion
{
    public partial class GenericTranDBContext : DbContext, IDbContextBase
    {
        public GenericTranDBContext(DbContextOptions<GenericTranDBContext> options)
    : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {

        }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.SrNo);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.State).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public DbContext dbContext { get; set; }
    }
}
