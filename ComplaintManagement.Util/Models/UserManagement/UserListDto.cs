using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.UserManagement
{
    public class UserListDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Employee_Code { get; set; }
        public string Full_Name { get; set; }
        public int? Department { get; set; }
    }
}
