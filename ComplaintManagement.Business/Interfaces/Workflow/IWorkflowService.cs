using ComplaintManagement.Util.Models.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Interfaces.Workflow
{
    public interface IWorkflowService
    {
        Task<int> SaveAsync(WorkflowSaveModel model);
        Task<WorkflowSaveModel> GetAsync(int workflowId);
        Task<IEnumerable<WorkflowMasterModel>> ListAsync(int module);
    }
}
