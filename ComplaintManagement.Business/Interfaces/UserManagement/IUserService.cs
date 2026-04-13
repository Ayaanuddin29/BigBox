using ComplaintManagement.Util.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Interfaces.UserManagement
{
    public interface IUserService
    {
        Task CreateBusinessUserAsync(CreateBusinessUserDto dto);
        Task<List<UserListDto>> GetUsersAsync();
        Task<UserModel> GetProfileAsync(string userId);
        Task UpdateProfileAsync(UserModel model);
        Task<List<DepartmentDto>> GetDepartmentsAsync();
        Task<List<CommonDropdownDto>> GetDivisionsAsync();
        Task<List<CommonDropdownDto>> GetRegionsAsync();
        Task<List<CommonDropdownDto>> GetCountriesAsync();
        Task<List<CommonDropdownDto>> GetStatesAsync();
        Task<List<CommonDropdownDto>> GetCitiesAsync();
        Task<List<CommonDropdownDto>> GetLocationsAsync();
    }
}
