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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<int> CreateAsync(CategoryModel model)
            => await _repo.CreateAsync(model);

        public async Task<int> UpdateAsync(CategoryModel model)
            => await _repo.UpdateAsync(model);

        public async Task<int> DeleteAsync(int id)
            => await _repo.DeleteAsync(id);
    }
}
