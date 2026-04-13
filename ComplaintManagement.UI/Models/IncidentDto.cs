namespace ComplaintManagement.UI.Models
{
    public class IncidentDto
    {
        public string Number { get; set; }
        public string Caller { get; set; }
        public int Category { get; set; }
        public int SubCategory { get; set; }
        public int Service { get; set; }
        public string ShortDescription { get; set; }
        public string Description{get; set; }
        public string ShortName { get; set; }
        public string State { get; set; }
        public string Impact { get; set; }
        public string Urgency { get; set; }
        public string Priority { get; set; }
        public string AssignedTo { get; set; }
    }
}
