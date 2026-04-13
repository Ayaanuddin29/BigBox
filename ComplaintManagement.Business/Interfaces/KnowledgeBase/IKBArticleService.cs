using ComplaintManagement.Util.Models.KnowledgeBase;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Interfaces.KnowledgeBase
{
    public interface IKBArticleService
    {
        Task<int> CreateKBArticleAsync(KBArticleCreateDto dto);
        Task<List<KBArticleListDto>> GetKBArticleListAsync();
        Task<KBArticleCreateDto> GetKBArticleByIdAsync(int id);
        Task UpdateKBArticleAsync(int id, KBArticleCreateDto dto);
    }

}