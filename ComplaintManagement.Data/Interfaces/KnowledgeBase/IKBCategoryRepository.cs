using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ComplaintManagement.Util.Models.KnowledgeBase;

namespace ComplaintManagement.Data.Interfaces.KnowledgeBase
{
    public interface IKBCategoryRepository
    {
        Task<IEnumerable<KBCategoryModel>> GetAllAsync();
        Task<int> CreateAsync(KBCategoryModel model);
        Task<int> UpdateAsync(KBCategoryModel model);
        Task<int> DeleteAsync(int id);
    }
}