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
    public class ProcessRelationService : IProcessRelationService
    {
        private readonly IProcessRelationRepository _repo;

        public ProcessRelationService(IProcessRelationRepository repo)
        {
            _repo = repo;
        }
       
        public async Task<IEnumerable<TicketDto>> GetTickets()
        {
            return await _repo.GetTickets();
        }

        public async Task AddRelations(int parentId, List<int> childIds, int relationTypeId)
        {
            var relations = childIds.Select(x => new ProcessRelationDto
            {
                ParentId = parentId,
                ChildId = x,
                ParentModule = 1,
                ChildModule = 1,
                AllowUpdates = true,
                RelationTypeId = relationTypeId,
                Comp = 1,
                LoginCreated = "Admin"
            }).ToList();

            await _repo.AddRelation(relations);
        }

        public async Task<IEnumerable<RelationViewDto>> GetRelations(int parentId)
        {
            return await _repo.GetRelations(parentId);
        }
    }
}
