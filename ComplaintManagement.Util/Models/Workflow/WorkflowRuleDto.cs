using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Workflow
{
    public class WorkflowRuleDto
    {
        public int RuleNumber { get; set; }
        public string? RuleName { get; set; }
        public int? OrderTypeId { get; set; }
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
        public bool? Active { get; set; }       
        public DateTime? RecCreated { get; set; }
        public string? LoginCreated { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LoginModified { get; set; }

    }
}
