using ComplaintManagement.Util.Models.Incident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Interfaces.Incident
{
    public interface IProcessRelationService
    {

        Task<IEnumerable<TicketDto>> GetTickets();

        Task AddRelations(int parentId, List<int> childIds, int relationTypeId);

        Task<IEnumerable<RelationViewDto>> GetRelations(int parentId);
    }
}
