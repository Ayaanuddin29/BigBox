using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.UserManagement
{
    public class CreateBusinessUserDto
    {
        public string? UserId { get; set; }   // From Identity
        public string? Employee_Code { get; set; }
        public string? Email { get; set; }
        public string? Full_Name { get; set; }
        public string? Department { get; set; }

        public int? Salutation { get; set; }
        public int? Job_title { get; set; }
        public int? Role { get; set; }
        public int? Vip { get; set; }

        public string? Phone { get; set; }
        public string? Phone_Ext { get; set; }
        public string? Mobile { get; set; }

        public bool? Account_Locked { get; set; }
        public int? Manager { get; set; }
        public int? Access_Type { get; set; }
        public int? Time_Zone { get; set; }
        public int? User_Group { get; set; }
        public int? Language_For_Support { get; set; }

        public bool? Email_Alert { get; set; }
        public bool? Sms_Alert { get; set; }

        public int? Division { get; set; }
        public int? Region { get; set; }
        public int? Branch { get; set; }
        public int? Location { get; set; }

        public string? City { get; set; }
        public int? State { get; set; }
        public int? Country { get; set; }
        public string? Zip { get; set; }
        public string? Address { get; set; }

        public string? Secret_Question { get; set; }
        public string? Secret_Answer { get; set; }

        public DateTime? Rec_Created { get; set; }
        public bool? Active { get; set; }
    }
}