using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplaintManagement.Util.Models.KnowledgeBase;

namespace ComplaintManagement.Data.Interfaces.KnowledgeBase
{
    public interface IKBStatusRepository
    {
        Task<IEnumerable<KBStatusModel>> GetAllAsync();
        Task<int> CreateAsync(KBStatusModel model);
        Task<int> UpdateAsync(KBStatusModel model);
        Task<int> DeleteAsync(int id);
    }
}