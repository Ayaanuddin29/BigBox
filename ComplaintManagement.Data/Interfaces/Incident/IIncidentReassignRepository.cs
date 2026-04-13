using ComplaintManagement.Util.Models.Incident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.Incident
{
    public interface IIncidentReassignRepository
    {
        Task<IncidentReassignModel> GetIncidentDetails(int incidentId);

        Task<bool> UpdateAssignment(IncidentReassignModel model);

        Task<bool> UpdatePrioritySlt(IncidentReassignModel model);

        Task<bool> UpdateCategorization(IncidentReassignModel model);
    }
}
