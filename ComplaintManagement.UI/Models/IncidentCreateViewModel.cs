using ComplaintManagement.Util.Models.Incident;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ComplaintManagement.UI.Models
{
    public class IncidentCreateViewModel
    {
        public string? Number { get; set; }
        [Required(ErrorMessage = "Caller is required")]
        public string? Caller { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public int?  Category { get; set; }
        [ValidateNever]
        public string? CategoryText { get; set; }

        [Required(ErrorMessage = "SubCategory is required")]
        public int? SubCategory { get; set; }
        [Required(ErrorMessage = "Service is required")]
        public int? Service { get; set; }
        public string? ServiceOffering { get; set; }
        public string? ConfigurationItem { get; set; }
        [Required(ErrorMessage = "Short Description is required")]
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public string? Channel { get; set; }
        public string? State { get; set; }
        public int? Impact { get; set; }
        public int? Urgency { get; set; }
        public int? Priority { get; set; }
        [ValidateNever]
        public string? PriorityText { get; set; }
        public int? AssignmentGroup { get; set; }
        public int? AssignedTo { get; set; }
        [ValidateNever]
        public List<AuditIncidentDto>? AuditLogs { get; set; }
    }

    public class IncidentAuditViewModel
    {
        public string EventType { get; set; }   // Created, StatusChange, AssignmentChange, PriorityChange, FieldUpdate
        public string Title { get; set; }
        public string Description { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
