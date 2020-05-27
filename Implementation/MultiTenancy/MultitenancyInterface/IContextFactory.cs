using Abstraction.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.MultiTenancy.MultitenancyInterface
{
    public interface IContextFactory
    {
        IDbContextBase DbContext { get; }

        string DatabaseName { get; set; }

        string ServerPathName { get; set; }
    }
}
