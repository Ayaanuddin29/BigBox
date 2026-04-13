using ComplaintManagement.Util.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Interfaces.Master
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryModel>> GetAllAsync();
        Task<int> CreateAsync(CategoryModel model);
        Task<int> UpdateAsync(CategoryModel model);
        Task<int> DeleteAsync(int id);

    }
}
