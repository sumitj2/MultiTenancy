using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.MultiTenancy
{
    public class ConnectionSettings
    {
       
        public DatabaseType DatabaseType { get; set; }
       
        public string DefaultConnection { get; set; }
    }
}
