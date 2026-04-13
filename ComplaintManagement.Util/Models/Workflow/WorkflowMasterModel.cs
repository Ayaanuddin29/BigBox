using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Workflow
{
    public class WorkflowMasterModel
    {
        public int WorkflowId { get; set; }
        public string WorkflowName { get; set; }
        public int Module { get; set; }
        public bool Active { get; set; }
    }
}
