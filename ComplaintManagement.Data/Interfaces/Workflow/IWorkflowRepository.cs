using ComplaintManagement.Util.Models.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.Workflow
{
    public interface IWorkflowRepository
    {
        Task<int> CreateAsync(WorkflowSaveModel model);
        Task UpdateAsync(WorkflowSaveModel model);
        Task<WorkflowSaveModel> GetByIdAsync(int workflowId);
        Task<IEnumerable<WorkflowMasterModel>> GetAllAsync(int module);
    }
}
