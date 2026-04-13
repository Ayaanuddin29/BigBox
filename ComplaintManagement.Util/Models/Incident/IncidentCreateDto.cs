using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Incident
{
    public class IncidentCreateDto
    {
        public int? TypeId { get; set; }
        public int? SourceId { get; set; }
        public string? AffectedUserId { get; set; }
        public string? CreatedByUserId { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }

        public int? ClassificationId { get; set; }
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? ServiceId { get; set; }

        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }
        public int? UrgencyId { get; set; }
        public int? ImpactId { get; set; }

        public string? Summary { get; set; }
        public string? Description { get; set; }

        public int? AssignedByAssociateId { get; set; }
        public int? AssignedToAssociateId { get; set; }
        public int? AssignedToGroupId { get; set; }

        public int? Comp { get; set; }
        public string? LoginCreated { get; set; }

        public string? Longitude { get; set; }
        public string? Latitude { get; set; }

        // extra flexible columns
        public string? Column1 { get; set; }
        public string? Column2 { get; set; }
        public string? Column3 { get; set; }
    }
}
