using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Incident
{
    public class IncidentListDto
    {
        public string Number { get; set; }
        public string Caller { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Service { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string Impact { get; set; }
        public string Urgency { get; set; }
        public string Priority { get; set; }
        public string AssignedTo { get; set; }
    }
}
