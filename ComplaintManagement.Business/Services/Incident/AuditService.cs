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
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _repo;

        public AuditService(IAuditRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> InsertAsync(AuditIncidentDto model)
        {
            model.Rec_Created = DateTime.Now;
            return await _repo.InsertAsync(model);
        }

        public async Task<IEnumerable<AuditIncidentDto>> GetByIncidentAsync(int inciNo)
        {
            return await _repo.GetByIncidentAsync(inciNo);
        }
    }
}
