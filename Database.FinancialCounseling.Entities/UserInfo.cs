using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Entities
{
    public partial class UserInfo
    {
        public int SrNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }
    }
}
