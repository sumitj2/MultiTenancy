using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.CommonInterfaces
{
    public interface ITransactionRepository<T>  where T: class 
    {
        IDbContextBase dbContextBase { get; set; }
        DbSet<T> dbSetBase { get; set; }
    }
}
