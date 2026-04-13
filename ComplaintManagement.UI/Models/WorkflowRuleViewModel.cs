using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComplaintManagement.UI.Models
{
    public class WorkflowRuleViewModel
    {
        public int RuleNumber { get; set; }
        public string RuleName { get; set; }

        public int OrderTypeId { get; set; }
        public int? CategoryId { get; set; }
        public int? ServiceTypeId { get; set; }
        public int? ServiceItemId { get; set; }
        public int? WorkflowUnitId { get; set; }
        public int? LanguageId { get; set; }
        public int? PriorityId { get; set; }

        public int? SltHours { get; set; }

        public int? AssignToAssociateId { get; set; }
        public int? AssignToGroupId { get; set; }
        public int? AssociateTypeId { get; set; }

        //// Drop-down lists
        //public IEnumerable<SelectListItem> OrderTypes { get; set; } = Enumerable.Empty<SelectListItem>();
        //public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        //public IEnumerable<SelectListItem> Priorities { get; set; } = Enumerable.Empty<SelectListItem>();
        //// ... etc ...
    }
}
