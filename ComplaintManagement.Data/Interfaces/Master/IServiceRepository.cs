using ComplaintManagement.Util.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.Master
{
    public interface IServiceRepository
    {
        Task<IEnumerable<ServiceModel>> GetAllAsync();
        Task<IEnumerable<ServiceModel>> GetBySubCategoryAsync(int subCategoryId);
        Task<int> CreateAsync(ServiceModel model);
        Task<int> UpdateAsync(ServiceModel model);
        Task<int> DeleteAsync(int id);
    }
}
