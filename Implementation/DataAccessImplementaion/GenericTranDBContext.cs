using Abstraction.CommonInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.DataAccessImplementaion
{
    public class GenericTranDBContext : DbContext, IDbContextBase
    {
        public GenericTranDBContext(DbContextOptions<GenericTranDBContext> options)
    : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
        }

        public DbContext dbContext { get; set; }
    }
}
