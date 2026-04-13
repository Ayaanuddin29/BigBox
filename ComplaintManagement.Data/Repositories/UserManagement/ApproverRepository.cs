using ComplaintManagement.Data.Interfaces.UserManagement;
using ComplaintManagement.Util.Models.UserManagement;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Repositories.UserManagement
{
    public class ApproverRepository:IApproverRepository
    {
        private readonly IDbConnection _db;

        public ApproverRepository(IConfiguration config)
        {
            _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<ApproverDto>> GetAllAsync()
        {
            string sql = "SELECT manager_code AS ManagerCode ,manager_name AS ManagerName,manager_name_2,manager_name_3,username,active,availability,alt_mgr_username AS AltMgrUsername,rec_created,login_created,login_modified,last_modified FROM robox_m_manager ORDER BY manager_code DESC";
            return await _db.QueryAsync<ApproverDto>(sql);
        }

        public async Task<ApproverDto?> GetByIdAsync(int id)
        {
            string sql = "SELECT manager_code AS ManagerCode ,manager_name AS ManagerName,manager_name_2,manager_name_3,username,active,availability,alt_mgr_username AS AltMgrUsername,rec_created,login_created,login_modified,last_modified FROM robox_m_manager WHERE manager_code = @Id";
            return await _db.QueryFirstOrDefaultAsync<ApproverDto>(sql, new { Id = id });
        }

        public async Task<int> CreateAsync(ApproverDto dto)
        {
            string sql = @"INSERT INTO robox_m_manager
                      (manager_name, manager_name_2, manager_name_3,
                       username, active, availability,
                       alt_mgr_username, rec_created, login_created)
                      VALUES
                      (@ManagerName, @ManagerName2, @ManagerName3,
                       @Username, @Active, @Availability,
                       @AltMgrUsername, GETDATE(), @LoginCreated)";

            return await _db.ExecuteAsync(sql, dto);
        }

        public async Task<int> UpdateAsync(ApproverDto dto)
        {
            string sql = @"UPDATE robox_m_manager SET
                        manager_name = @ManagerName,
                        manager_name_2 = @ManagerName2,
                        manager_name_3 = @ManagerName3,
                        active = @Active,
                        availability = @Availability,
                        alt_mgr_username = @AltMgrUsername,
                        login_modified = @LoginModified,
                        last_modified = GETDATE()
                       WHERE manager_code = @ManagerCode";

            return await _db.ExecuteAsync(sql, dto);
        }

        public async Task<ApproverDto?> GetByUsernameAsync(string username)
        {
            string sql = "SELECT * FROM robox_m_manager WHERE username = @username";
            return await _db.QueryFirstOrDefaultAsync<ApproverDto>(sql, new { username });
        }

        public async Task InsertAsync(ApproverDto dto)
        {
            string sql = @"INSERT INTO robox_m_manager
                       (manager_name, username, active, availability, rec_created, login_created)
                       VALUES
                       (@ManagerName, @Username, 1, 1, GETDATE(), @LoginCreated)";

            await _db.ExecuteAsync(sql, dto);
        }

        public async Task ActivateAsync(string username)
        {
            string sql = @"UPDATE robox_m_manager 
                       SET active = 1, availability = 1, last_modified = GETDATE()
                       WHERE username = @username";

            await _db.ExecuteAsync(sql, new { username });
        }

        public async Task DeactivateAsync(string username)
        {
            string sql = @"UPDATE robox_m_manager 
                       SET active = 0, availability = 0, last_modified = GETDATE()
                       WHERE username = @username";

            await _db.ExecuteAsync(sql, new { username });
        }

        public async Task<IEnumerable<ApproverDto>> GetAvailableManagersAsync(string excludeUsername)
        {
            string sql = @"
        SELECT 
            manager_code   AS ManagerCode,
            manager_name   AS ManagerName,
            username       AS Username
        FROM robox_m_manager
        WHERE active = 1
          AND availability = 1
          AND username <> @ExcludeUsername
        ORDER BY manager_name";

            return await _db.QueryAsync<ApproverDto>(sql, new { ExcludeUsername = excludeUsername });
        }
    }
}
