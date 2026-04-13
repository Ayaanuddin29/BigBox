using ComplaintManagement.Util.Models.Incident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.Incident
{
    public interface IAuditRepository
    {
        Task<int> InsertAsync(AuditIncidentDto model);
        Task<IEnumerable<AuditIncidentDto>> GetByIncidentAsync(int inciNo);
    }
}
