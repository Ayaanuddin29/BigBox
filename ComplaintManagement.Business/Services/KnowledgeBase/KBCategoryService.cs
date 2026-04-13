using ComplaintManagement.Business.Interfaces.KnowledgeBase;
using ComplaintManagement.Data.Interfaces.KnowledgeBase;
using ComplaintManagement.Util.Models.KnowledgeBase;

namespace ComplaintManagement.Business.Services.KnowledgeBase
{
    public class KBCategoryService : IKBCategoryService
    {
        private readonly IKBCategoryRepository _repo;

        public KBCategoryService(IKBCategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<KBCategoryModel>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<int> CreateAsync(KBCategoryModel model)
            => await _repo.CreateAsync(model);

        public async Task<int> UpdateAsync(KBCategoryModel model)
            => await _repo.UpdateAsync(model);

        public async Task<int> DeleteAsync(int id)
            => await _repo.DeleteAsync(id);
    }
}