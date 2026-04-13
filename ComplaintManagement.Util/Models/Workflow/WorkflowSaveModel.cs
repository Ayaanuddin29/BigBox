using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Workflow
{
    public class WorkflowSaveModel
    {
        public int WorkflowId { get; set; }
        public string WorkflowName { get; set; }
        public int Module { get; set; }
        public string WorkflowJson { get; set; }
        public string ConditionJson { get; set; }
        public string LoginCreated { get; set; }
    }

    public class ConditionGroup
    {
        public string Logic { get; set; } = "AND";
        public List<ConditionRule> Rules { get; set; }
    }

    public class ConditionRule
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
    }
}
