using System;

namespace ComplaintManagement.Util.Models.UserManagement
{
    public class RoboxUser
    {
        public int? Id { get; set; }

        public string? UserId { get; set; }

        public string? Employee_Code { get; set; }

        public string? Full_Name { get; set; }

        public int? Department { get; set; }
        public string? Department_Name { get; set; }

        public string? Email { get; set; }

        public bool? Active { get; set; }

        public DateTime? Rec_Created { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public int? Division { get; set; }

        public int? Region { get; set; }

        public int? Location { get; set; }

        public int? City { get; set; }

        public int? State { get; set; }

        public int? Country { get; set; }

        public string? Zip { get; set; }

        public bool? Email_Alert { get; set; }

        public bool? Sms_Alert { get; set; }

        public string? Secret_Question { get; set; }

        public string? Secret_Answer { get; set; }

        // ✅ Name fields from JOIN tables
        public string? Division_Name { get; set; }

        public string? Region_Name { get; set; }

        public string? Country_Name { get; set; }

        public string? State_Name { get; set; }

        public string? City_Name { get; set; }

        public string? Location_Name { get; set; }
    }
}