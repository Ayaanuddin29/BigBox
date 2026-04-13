namespace ComplaintManagement.Util.Models.UserManagement
{
    public class ProfileDropdownDto
    {
        public List<CommonDropdownDto> divisions { get; set; }

        public List<CommonDropdownDto> regions { get; set; }

        public List<CommonDropdownDto> countries { get; set; }

        public List<CommonDropdownDto> states { get; set; }

        public List<CommonDropdownDto> cities { get; set; }

        public List<CommonDropdownDto> locations { get; set; }
    }
}