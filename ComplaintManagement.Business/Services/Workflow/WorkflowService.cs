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
    public class WorkflowService : IWorkflowService
    {
        private readonly IWorkflowRepository _repo;

        public WorkflowService(IWorkflowRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> SaveAsync(WorkflowSaveModel model)
        {
            if (model.WorkflowId == 0)
                return await _repo.CreateAsync(model);

            await _repo.UpdateAsync(model);
            return model.WorkflowId;
        }

        public Task<WorkflowSaveModel> GetAsync(int workflowId)
            => _repo.GetByIdAsync(workflowId);

        public Task<IEnumerable<WorkflowMasterModel>> ListAsync(int module)
            => _repo.GetAllAsync(module);
    }
}
