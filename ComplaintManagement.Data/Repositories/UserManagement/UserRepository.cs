using ComplaintManagement.Data.Interfaces.UserManagement;
using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ComplaintManagement.Data.Repositories.UserManagement
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
            => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        public async Task<IEnumerable<RoboxUser>> GetAllAsync()
        {
            using var db = CreateConnection();
            return await db.QueryAsync<RoboxUser>(
                "ProcGet_Users_List_All",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<RoboxUser?> GetByUserIdAsync(string userId)
        {
            using var db = CreateConnection();

            var result = await db.QueryAsync<RoboxUser>(
                "ProcGet_Users_By_Userid",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure);

            return result.FirstOrDefault();
        }

        public async Task InsertAsync(RoboxUser user)
        {
            using var db = CreateConnection();
            await db.ExecuteAsync(
                "ProcInsert_Users_Create",
                new
                {
                    UserId = user.UserId,
                    Employee_Code = user.Employee_Code,
                    Email = user.Email,
                    Full_Name = user.Full_Name,
                    Department = user.Department,
                    Rec_Created = user.Rec_Created
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateAsync(RoboxUser user)
        {
            try
            {
                using var db = CreateConnection();

                await db.ExecuteAsync(
                    "ProcUpdate_Users_Details1",
                    new
                    {
                        UserId = user.UserId,
                        Email = user.Email,
                        Phone = user.Phone,
                        Address = user.Address,

                        Department = user.Department,
                        Division = user.Division,
                        Region = user.Region,
                        Location = user.Location,
                        City = user.City,
                        State = user.State,
                        Country = user.Country,

                        Zip = user.Zip,

                        Email_Alert = user.Email_Alert,
                        Sms_Alert = user.Sms_Alert,

                        Secret_Question = user.Secret_Question,
                        Secret_Answer = user.Secret_Answer
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user: " + ex.Message);
            }
        }
        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
        {
            using var db = CreateConnection();

            return await db.QueryAsync<DepartmentDto>(
                "ProcGet_Department_List",
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<CommonDropdownDto>> GetDivisionsAsync()
        {
            using var db = CreateConnection();
            return await db.QueryAsync<CommonDropdownDto>("ProcGet_Division_List",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CommonDropdownDto>> GetRegionsAsync()
        {
            using var db = CreateConnection();
            return await db.QueryAsync<CommonDropdownDto>("ProcGet_Region_List",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CommonDropdownDto>> GetCountriesAsync()
        {
            using var db = CreateConnection();
            return await db.QueryAsync<CommonDropdownDto>("ProcGet_Country_List",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CommonDropdownDto>> GetStatesAsync()
        {
            using var db = CreateConnection();
            return await db.QueryAsync<CommonDropdownDto>("ProcGet_State_List",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CommonDropdownDto>> GetCitiesAsync()
        {
            using var db = CreateConnection();
            return await db.QueryAsync<CommonDropdownDto>("ProcGet_City_List",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<CommonDropdownDto>> GetLocationsAsync()
        {
            using var db = CreateConnection();
            return await db.QueryAsync<CommonDropdownDto>("ProcGet_Location_List",
                commandType: CommandType.StoredProcedure);
        }
    }
}
