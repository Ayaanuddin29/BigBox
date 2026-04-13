using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.UserManagement
{
    public class ManageUserRolesDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleSelectionDto> Roles { get; set; }
    }

    public class RoleSelectionDto
    {
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }

}
