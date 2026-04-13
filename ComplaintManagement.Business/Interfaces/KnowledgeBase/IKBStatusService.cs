using ComplaintManagement.Util.Models.KnowledgeBase;

namespace ComplaintManagement.Business.Interfaces.KnowledgeBase
{
    public interface IKBStatusService
    {
        Task<IEnumerable<KBStatusModel>> GetAllAsync();
        Task<int> CreateAsync(KBStatusModel model);
        Task<int> UpdateAsync(KBStatusModel model);
        Task<int> DeleteAsync(int id);
    }
}