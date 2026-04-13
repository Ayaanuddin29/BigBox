using ComplaintManagement.Util.Models.SLM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Interfaces.SLM
{
    public interface ISltService
    {
        Task<IEnumerable<SltDto>> GetAllAsync();
        Task<SltDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(SltDto dto, string login);
        Task<bool> UpdateAsync(SltDto dto, string login);
        Task<bool> DeleteAsync(int id);
    }
}
