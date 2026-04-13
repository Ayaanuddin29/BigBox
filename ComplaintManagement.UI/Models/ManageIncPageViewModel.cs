using ComplaintManagement.Util.Models.Incident;

namespace ComplaintManagement.UI.Models
{
    public class ManageIncPageViewModel
    {
        public ActivityDto ? formIncActivity { get; set; }
        public ManageIncidentViewModel? formManageInc { get; set; }

        public List<ActivityReadDto>? formActivityRead { get; set; }

        public IncidentTasksDto ? formIncidentTasksDto{get; set; }

        public List<IncidentTaskReadDto>? formTaskReadDto { get; set; }
        public List<AuditIncidentDto>? formIncidentAuditDto { get; set; }

    }
}
