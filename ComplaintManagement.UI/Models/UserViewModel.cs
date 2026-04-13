namespace ComplaintManagement.UI.Models
{
    public class UserViewModel
    {
        public int? Id { get; set; }

        public string? username { get; set; }

        public string? email { get; set; }

        public string? phone { get; set; }

        public int? division { get; set; }

        public int? department { get; set; }
        public string? department_name { get; set; }

        public int? region { get; set; }

        public int? location { get; set; }

        public int? city { get; set; }

        public int? state { get; set; }

        public int? country { get; set; }

        public string? zip { get; set; }

        public string? address { get; set; }

        public bool email_alert { get; set; }

        public bool sms_alert { get; set; }

        public string? secret_question { get; set; }

        public string? secret_answer { get; set; }

        // ✅ NEW NAME FIELDS
        public string? division_name { get; set; }

        public string? region_name { get; set; }

        public string? country_name { get; set; }

        public string? state_name { get; set; }

        public string? city_name { get; set; }

        public string? location_name { get; set; }
    }
}