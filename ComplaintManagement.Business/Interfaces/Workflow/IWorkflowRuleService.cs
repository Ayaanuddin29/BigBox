using ComplaintManagement.Util.Models.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Interfaces.Workflow
{
    public interface IWorkflowRuleService
    {
        Task<IEnumerable<WorkflowRuleDto>> GetAllAsync();
        Task<WorkflowRuleDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(WorkflowRuleDto model, string loginCreated);
        Task<bool> UpdateAsync(WorkflowRuleDto model, string loginModified);
        Task<bool> DeleteAsync(int id);
    }
}
