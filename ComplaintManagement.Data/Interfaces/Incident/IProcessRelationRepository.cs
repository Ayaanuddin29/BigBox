using ComplaintManagement.Util.Models.Incident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.Incident
{
    public interface IProcessRelationRepository
    {       

        Task<IEnumerable<TicketDto>> GetTickets();

        Task<int> AddRelation(List<ProcessRelationDto> relations);

        Task<IEnumerable<RelationViewDto>> GetRelations(int parentId);

    }
}
