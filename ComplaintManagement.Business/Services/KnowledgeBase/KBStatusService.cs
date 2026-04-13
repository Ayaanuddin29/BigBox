using ComplaintManagement.Business.Interfaces.KnowledgeBase;
using ComplaintManagement.Data.Interfaces.KnowledgeBase;
using ComplaintManagement.Util.Models.KnowledgeBase;

namespace ComplaintManagement.Business.Services.KnowledgeBase
{
    public class KBStatusService : IKBStatusService
    {
        private readonly IKBStatusRepository _repo;

        public KBStatusService(IKBStatusRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<KBStatusModel>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<int> CreateAsync(KBStatusModel model)
            => await _repo.CreateAsync(model);

        public async Task<int> UpdateAsync(KBStatusModel model)
            => await _repo.UpdateAsync(model);

        public async Task<int> DeleteAsync(int id)
            => await _repo.DeleteAsync(id);
    }
}