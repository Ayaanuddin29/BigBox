using ComplaintManagement.Business.Interfaces.Master;
using ComplaintManagement.Data.Interfaces.Master;
using ComplaintManagement.Util.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Services.Master
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repo;

        public ServiceService(IServiceRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ServiceModel>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<IEnumerable<ServiceModel>> GetBySubCategoryAsync(int subCategoryId)
            => await _repo.GetBySubCategoryAsync(subCategoryId);

        public async Task<int> CreateAsync(ServiceModel model)
            => await _repo.CreateAsync(model);

        public async Task<int> UpdateAsync(ServiceModel model)
            => await _repo.UpdateAsync(model);

        public async Task<int> DeleteAsync(int id)
            => await _repo.DeleteAsync(id);
    }
}
