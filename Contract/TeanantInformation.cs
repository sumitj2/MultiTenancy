using System;
using System.Collections.Generic;
using System.Text;

namespace Contract
{
    public class TenantInformation
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
