using ComplaintManagement.Business.Interfaces.KnowledgeBase;
using ComplaintManagement.Data.Interfaces.KnowledgeBase;
using ComplaintManagement.Util.Models.KnowledgeBase;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Services.KnowledgeBase
{
    public class KBArticleService : IKBArticleService
    {
        private readonly IKBArticleRepository _repository;

        public KBArticleService(IKBArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateKBArticleAsync(KBArticleCreateDto dto)
        {
            return await _repository.InsertKBArticleAsync(dto);
        }
        public async Task<List<KBArticleListDto>> GetKBArticleListAsync()
        {
            return await _repository.GetKBArticleListAsync();
        }
        public async Task<KBArticleCreateDto> GetKBArticleByIdAsync(int id)
        {
            return await _repository.GetKBArticleByIdAsync(id);
        }

        public async Task UpdateKBArticleAsync(int id, KBArticleCreateDto dto)
        {
            await _repository.UpdateKBArticleAsync(id, dto);
        }
    }
}