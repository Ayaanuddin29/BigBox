using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Incident
{
    public class IncidentUpdateDto
    {
        public string? incidentId { get; set; }      
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? ServiceId { get; set; }

        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }
        public int? UrgencyId { get; set; }
        public int? ImpactId { get; set; }

        public string? Summary { get; set; }
        public string? Description { get; set; }
        public string? AssignedToAssociateId { get; set; }
    }
}
