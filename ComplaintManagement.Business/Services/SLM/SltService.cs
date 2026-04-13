using ComplaintManagement.Business.Interfaces.SLM;
using ComplaintManagement.Data.Interfaces.SLM;
using ComplaintManagement.Util.Models.SLM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Services.SLM
{
    public class SltService : ISltService
    {
        private readonly ISltRepository _repo;

        public SltService(ISltRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<SltDto>> GetAllAsync() => _repo.GetAllAsync();
        public Task<SltDto?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<int> CreateAsync(SltDto dto, string login) => _repo.InsertAsync(dto, login);
        public Task<bool> UpdateAsync(SltDto dto, string login) => _repo.UpdateAsync(dto, login);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
