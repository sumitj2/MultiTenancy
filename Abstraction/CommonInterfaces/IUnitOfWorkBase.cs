using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.CommonInterfaces
{
    public interface IUnitOfWorkBase
    {
       void Commit();
    }
}
