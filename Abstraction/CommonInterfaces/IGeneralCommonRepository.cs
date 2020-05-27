using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.CommonInterfaces
{
    interface IGeneralCommonRepository<T> :IDbContextBase, IRepositoryBase<T> where T : class
    {
    }
}
