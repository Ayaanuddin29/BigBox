using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.UserManagement
{
    public class ApproverDto
    {
        public int ManagerCode { get; set; }

        public string? ManagerName { get; set; }
        public string? ManagerName2 { get; set; }
        public string? ManagerName3 { get; set; }

        public string Username { get; set; } = null!;

        public bool Active { get; set; }
        public bool Availability { get; set; }

        public string? AltMgrUsername { get; set; }

        public DateTime? RecCreated { get; set; }
        public string? LoginCreated { get; set; }
        public string? LoginModified { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
