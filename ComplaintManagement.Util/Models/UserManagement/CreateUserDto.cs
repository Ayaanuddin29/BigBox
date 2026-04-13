using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.UserManagement
{
    public class CreateUserDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public string? Employee_Code { get; set; }
        public string? Full_Name { get; set; }
        public string? Department { get; set; }
        public string? Role { get; set; }

        public int? Salutation { get; set; }
        public int? Job_title { get; set; }
        public int? Vip { get; set; }

        public string? Phone { get; set; }
        public string? Phone_Ext { get; set; }
        public string? Mobile { get; set; }

        public int? Manager { get; set; }
        public int? Access_Type { get; set; }
        public int? Time_Zone { get; set; }

        public int? Division { get; set; }
        public int? Region { get; set; }
        public int? Branch { get; set; }
        public int? Location { get; set; }

        public string? City { get; set; }
        public int? State { get; set; }
        public int? Country { get; set; }
        public string? Zip { get; set; }
        public string? Address { get; set; }
    }

}
