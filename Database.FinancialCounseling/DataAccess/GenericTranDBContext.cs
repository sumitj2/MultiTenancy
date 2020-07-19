
using Database.Abstraction.Common;
using Database.Common;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public  class GenericTranDBContext : DbContext, IDbContext
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

           // OnModelCreatingPartial(modelBuilder);
        }
       // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public DbContext dbContext { get; set; }

       

        

    }
}
