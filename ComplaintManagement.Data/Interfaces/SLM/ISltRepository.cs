using ComplaintManagement.Util.Models.SLM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.SLM
{
    public interface ISltRepository
    {
        Task<IEnumerable<SltDto>> GetAllAsync();
        Task<SltDto?> GetByIdAsync(int id);
        Task<int> InsertAsync(SltDto model, string login);
        Task<bool> UpdateAsync(SltDto model, string login);
        Task<bool> DeleteAsync(int id);
    }
}
