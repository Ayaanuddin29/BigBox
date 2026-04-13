using ComplaintManagement.Business.Interfaces.UserManagement;
using ComplaintManagement.Data.Interfaces.UserManagement;
using ComplaintManagement.UI.Models;
using ComplaintManagement.Util.Models.UserManagement;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Diagnostics.Metrics;

namespace ComplaintManagement.Business.Services.UserManagement
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task CreateBusinessUserAsync(CreateBusinessUserDto dto)
        {
            await _userRepo.InsertAsync(new RoboxUser
            {
                UserId = dto.UserId,
                Employee_Code = dto.Employee_Code,
                Email = dto.Email,
                Full_Name = dto.Full_Name,
                //Department = dto.Department,
                Active = true,
                Rec_Created = DateTime.Now
            });
        }

        public async Task<List<UserListDto>> GetUsersAsync()
        {
            var users = await _userRepo.GetAllAsync();

            return users.Select(u => new UserListDto
            {
                UserId = u.UserId,
                Employee_Code = u.Employee_Code,
                Full_Name = u.Full_Name,
                Department = u.Department,
                Email = u.Email
            }).ToList();
        }

        // ✅ Profile Methods

        public async Task<UserModel> GetProfileAsync(string userId)
        {
            var user = await _userRepo.GetByUserIdAsync(userId);

            if (user == null) return null;

            return new UserModel
            {
                username = user.UserId,
                email = user.Email,
                phone = user.Phone,
                address = user.Address,
                zip = user.Zip,
                department = user.Department,
                department_name = user.Department_Name,
                division = user.Division,
                division_name = user.Division_Name,

                region = user.Region,
                region_name = user.Region_Name,

                country = user.Country,
                country_name = user.Country_Name,

                state = user.State,
                state_name = user.State_Name,

                city = user.City,
                city_name = user.City_Name,

                location = user.Location,
                location_name = user.Location_Name,

                email_alert = user.Email_Alert,
                sms_alert = user.Sms_Alert,
                secret_question = user.Secret_Question,
                secret_answer = user.Secret_Answer
            };
        }

        public async Task UpdateProfileAsync(UserModel model)
        {
            await _userRepo.UpdateAsync(new RoboxUser
            {
                UserId = model.username,
                Email = model.email,
                Phone = model.phone,
                Address = model.address,

                Department = model.department,
                Division = model.division,
                Region = model.region,
                Location = model.location,
                City = model.city,
                State = model.state,
                Country = model.country,

                Zip = model.zip,

                Email_Alert = model.email_alert ?? false,
                Sms_Alert = model.sms_alert ?? false,

                Secret_Question = model.secret_question,
                Secret_Answer = model.secret_answer
            });
        }
        public async Task<List<DepartmentDto>> GetDepartmentsAsync()
        {
            var data = await _userRepo.GetDepartmentsAsync();

            return data.ToList();
        }
        public async Task<List<CommonDropdownDto>> GetDivisionsAsync()
    => (await _userRepo.GetDivisionsAsync()).ToList();

        public async Task<List<CommonDropdownDto>> GetRegionsAsync()
            => (await _userRepo.GetRegionsAsync()).ToList();

        public async Task<List<CommonDropdownDto>> GetCountriesAsync()
            => (await _userRepo.GetCountriesAsync()).ToList();

        public async Task<List<CommonDropdownDto>> GetStatesAsync()
            => (await _userRepo.GetStatesAsync()).ToList();

        public async Task<List<CommonDropdownDto>> GetCitiesAsync()
            => (await _userRepo.GetCitiesAsync()).ToList();

        public async Task<List<CommonDropdownDto>> GetLocationsAsync()
            => (await _userRepo.GetLocationsAsync()).ToList();
    }
}