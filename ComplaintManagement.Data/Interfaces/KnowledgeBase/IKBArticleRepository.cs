using ComplaintManagement.Util.Models.KnowledgeBase;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.KnowledgeBase
{
    public interface IKBArticleRepository
    {
        Task<int> InsertKBArticleAsync(KBArticleCreateDto dto);
        Task<List<KBArticleListDto>> GetKBArticleListAsync();
        Task<KBArticleCreateDto> GetKBArticleByIdAsync(int id);
        Task UpdateKBArticleAsync(int id, KBArticleCreateDto dto);

    }
}