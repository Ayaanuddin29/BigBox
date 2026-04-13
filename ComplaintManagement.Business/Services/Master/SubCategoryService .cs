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
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _repo;

        public SubCategoryService(ISubCategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<SubCategoryModel>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<IEnumerable<SubCategoryModel>> GetByCategoryAsync(int categoryId)
            => await _repo.GetByCategoryAsync(categoryId);

        public async Task<int> CreateAsync(SubCategoryModel model)
            => await _repo.CreateAsync(model);

        public async Task<int> UpdateAsync(SubCategoryModel model)
            => await _repo.UpdateAsync(model);

        public async Task<int> DeleteAsync(int id)
            => await _repo.DeleteAsync(id);
    }
}
