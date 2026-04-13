using ComplaintManagement.Util.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.Master
{
    public interface ISubCategoryRepository
    {
        Task<IEnumerable<SubCategoryModel>> GetAllAsync();
        Task<IEnumerable<SubCategoryModel>> GetByCategoryAsync(int categoryId);
        Task<int> CreateAsync(SubCategoryModel model);
        Task<int> UpdateAsync(SubCategoryModel model);
        Task<int> DeleteAsync(int id);
    }
}
