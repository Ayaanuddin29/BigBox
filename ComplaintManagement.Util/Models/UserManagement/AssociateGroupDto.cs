using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.UserManagement
{
    public class AssociateGroupDto
    {
        public int AssociateGroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupEmailId { get; set; }
        public bool Active { get; set; }

        public List<int> SelectedStaffIds { get; set; } = new();
        public List<int> SelectedManagerIds { get; set; } = new();

        public List<AssociateLookupDto> StaffList { get; set; } = new();
        public List<AssociateLookupDto> ManagerList { get; set; } = new();
    }

    public class AssociateLookupDto
    {
        public int AssociateId { get; set; }
        public string AssociateName { get; set; }
    }
}
