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
    public class AssociateGroupRepository : IAssociateGroupRepository
    {
        private readonly IDbConnection _db;

        public AssociateGroupRepository(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        // ===============================
        // GROUP CRUD
        // ===============================

        public async Task<IEnumerable<AssociateGroupDto>> GetAllAsync()
        {
            var sql = @"SELECT 
                        associate_group_id AS AssociateGroupId,
                        group_name AS GroupName,
                        group_email_id AS GroupEmailId,
                        active AS Active
                    FROM robox_m_associate_group
                    ORDER BY associate_group_id DESC";

            return await _db.QueryAsync<AssociateGroupDto>(sql);
        }

        public async Task<AssociateGroupDto> GetByIdAsync(int id)
        {
            var sql = @"SELECT 
                        associate_group_id AS AssociateGroupId,
                        group_name AS GroupName,
                        group_email_id AS GroupEmailId,
                        active AS Active
                    FROM robox_m_associate_group
                    WHERE associate_group_id = @Id";

            return await _db.QueryFirstOrDefaultAsync<AssociateGroupDto>(sql, new { Id = id });
        }

        public async Task<int> CreateAsync(AssociateGroupDto model)
        {
            var sql = @"
            INSERT INTO robox_m_associate_group
            (group_name, group_email_id, active, rec_created)
            VALUES (@GroupName, @GroupEmailId, @Active, GETDATE());

            SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await _db.ExecuteScalarAsync<int>(sql, model);
        }

        public async Task UpdateAsync(AssociateGroupDto model)
        {
            var sql = @"
            UPDATE robox_m_associate_group
            SET group_name = @GroupName,
                group_email_id = @GroupEmailId,
                active = @Active,
                last_modified = GETDATE()
            WHERE associate_group_id = @AssociateGroupId";

            await _db.ExecuteAsync(sql, model);
        }

        // ===============================
        // ROLE BASED USERS
        // ===============================

        public async Task<IEnumerable<AssociateLookupDto>> GetUsersByRoleAsync(string roleName)
        {
            var sql = @"
            SELECT DISTINCT
                   a.associate_id AS AssociateId,
                   a.associate_name AS AssociateName
            FROM robox_m_associate a
            INNER JOIN AspNetUsers u ON a.username = u.UserName
            INNER JOIN AspNetUserRoles ur ON u.Id = ur.UserId
            INNER JOIN AspNetRoles r ON ur.RoleId = r.Id
            WHERE r.Name = @RoleName
              AND a.active = 1
            ORDER BY a.associate_name";

            return await _db.QueryAsync<AssociateLookupDto>(sql, new { RoleName = roleName });
        }

        // ===============================
        // STAFF LINKS
        // ===============================

        public async Task SaveStaffLinksAsync(int groupId, List<int> associateIds)
        {
            await _db.ExecuteAsync(
                "DELETE FROM robox_t_associate_group_link WHERE group_id = @groupId",
                new { groupId });

            if (associateIds == null || !associateIds.Any())
                return;

            foreach (var id in associateIds)
            {
                await _db.ExecuteAsync(
                    "INSERT INTO robox_t_associate_group_link (associate_id, group_id) VALUES (@associate_id, @group_id)",
                    new { associate_id = id, group_id = groupId });
            }
        }

        public async Task<List<int>> GetStaffIdsByGroupAsync(int groupId)
        {
            var sql = "SELECT associate_id FROM robox_t_associate_group_link WHERE group_id = @groupId";
            var result = await _db.QueryAsync<int>(sql, new { groupId });
            return result.ToList();
        }

        // ===============================
        // MANAGER LINKS
        // ===============================

        public async Task SaveManagerLinksAsync(int groupId, List<int> associateIds)
        {
            await _db.ExecuteAsync(
                "DELETE FROM robox_t_associatemgr_group_link WHERE group_id = @groupId",
                new { groupId });

            if (associateIds == null || !associateIds.Any())
                return;

            foreach (var id in associateIds)
            {
                await _db.ExecuteAsync(
                    "INSERT INTO robox_t_associatemgr_group_link (associate_id, group_id) VALUES (@associate_id, @group_id)",
                    new { associate_id = id, group_id = groupId });
            }
        }

        public async Task<List<int>> GetManagerIdsByGroupAsync(int groupId)
        {
            var sql = "SELECT associate_id FROM robox_t_associatemgr_group_link WHERE group_id = @groupId";
            var result = await _db.QueryAsync<int>(sql, new { groupId });
            return result.ToList();
        }
    }
}
