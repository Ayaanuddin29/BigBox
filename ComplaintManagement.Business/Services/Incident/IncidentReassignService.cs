using ComplaintManagement.Business.Interfaces.Incident;
using ComplaintManagement.Data.Interfaces.Incident;
using ComplaintManagement.Util.Models.Incident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Services.Incident
{
    public class IncidentReassignService : IIncidentReassignService
    {
        private readonly IIncidentReassignRepository _repo;

        public IncidentReassignService(IIncidentReassignRepository repo)
        {
            _repo = repo;
        }

        public async Task<IncidentReassignModel> GetIncidentDetails(int incidentId)
        {
            return await _repo.GetIncidentDetails(incidentId);
        }

        public async Task<bool> UpdateAssignment(IncidentReassignModel model)
        {
            return await _repo.UpdateAssignment(model);
        }

        public async Task<bool> UpdatePrioritySlt(IncidentReassignModel model)
        {
            return await _repo.UpdatePrioritySlt(model);
        }

        public async Task<bool> UpdateCategorization(IncidentReassignModel model)
        {
            return await _repo.UpdateCategorization(model);
        }
    }
}
