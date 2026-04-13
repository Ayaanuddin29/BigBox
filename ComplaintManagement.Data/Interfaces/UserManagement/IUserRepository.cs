using ComplaintManagement.Util.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.UserManagement
{
    public interface IUserRepository
    {
        Task<IEnumerable<RoboxUser>> GetAllAsync();
        Task<RoboxUser?> GetByUserIdAsync(string userId);
        Task InsertAsync(RoboxUser user);
        Task UpdateAsync(RoboxUser user);
        Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
        Task<IEnumerable<CommonDropdownDto>> GetDivisionsAsync();
        Task<IEnumerable<CommonDropdownDto>> GetRegionsAsync();
        Task<IEnumerable<CommonDropdownDto>> GetCountriesAsync();
        Task<IEnumerable<CommonDropdownDto>> GetStatesAsync();
        Task<IEnumerable<CommonDropdownDto>> GetCitiesAsync();
        Task<IEnumerable<CommonDropdownDto>> GetLocationsAsync();
    }
}
