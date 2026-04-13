using ComplaintManagement.Business.Interfaces.Workflow;
using ComplaintManagement.Data.Interfaces.Workflow;
using ComplaintManagement.Util.Models.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Services.Workflow
{
    public class WorkflowRuleService : IWorkflowRuleService
    {
        private readonly IWorkflowRuleRepository _repo;

        public WorkflowRuleService(IWorkflowRuleRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<WorkflowRuleDto>> GetAllAsync()
            => _repo.GetAllAsync();

        public Task<WorkflowRuleDto?> GetByIdAsync(int id)
            => _repo.GetByIdAsync(id);

        public Task<int> CreateAsync(WorkflowRuleDto model, string loginCreated)
            => _repo.InsertAsync(model, loginCreated);

        public Task<bool> UpdateAsync(WorkflowRuleDto model, string loginModified)
            => _repo.UpdateAsync(model, loginModified);

        public Task<bool> DeleteAsync(int id)
            => _repo.DeleteAsync(id);
    }
}
