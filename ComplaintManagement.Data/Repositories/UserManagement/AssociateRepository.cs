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
    public class AssociateRepository : IAssociateRepository
    {
        private readonly IConfiguration _configuration;


        public AssociateRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
           => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        public async Task<IEnumerable<AssociateDto>> GetAllAsync()
        {
            using var db = CreateConnection();

            string query = @"SELECT associate_id AS AssociateId,
                                associate_name AS AssociateName,
                                associate_name_2 AS AssociateName2,
                                associate_name_3 AS AssociateName3,
                                availability AS Availability,
                                alternate_associate AS AlternateAssociate,
                                active AS Active,
                                username AS Username,
                                rec_created AS RecCreated,
                                last_modified AS LastModified
                         FROM robox_m_associate";

            return await db.QueryAsync<AssociateDto>(query);
        }

        public async Task<AssociateDto> GetByIdAsync(int id)
        {
            using var db = CreateConnection();
            string query = @"SELECT associate_id AS AssociateId,
                                associate_name AS AssociateName,
                                associate_name_2 AS AssociateName2,
                                associate_name_3 AS AssociateName3,
                                availability AS Availability,
                                alternate_associate AS AlternateAssociate,
                                active AS Active,
                                username AS Username,
                                rec_created AS RecCreated,
                                last_modified AS LastModified
                         FROM robox_m_associate
                         WHERE associate_id = @Id";

            return await db.QueryFirstOrDefaultAsync<AssociateDto>(query, new { Id = id });
        }

        public async Task UpdateAsync(AssociateDto model)
        {
            using var db = CreateConnection();
            string query = @"UPDATE robox_m_associate
                         SET associate_name = @AssociateName,
                             associate_name_2 = @AssociateName2,
                             associate_name_3 = @AssociateName3,
                             availability = @Availability,
                             alternate_associate = @AlternateAssociate,
                             active = @Active,
                             last_modified = GETDATE()
                         WHERE associate_id = @AssociateId";

            await db.ExecuteAsync(query, model);
        }

        public async Task<AssociateDto> GetByUsernameAsync(string username)
        {
            using var db = CreateConnection();

            string query = @"SELECT TOP 1 *
                     FROM robox_m_associate
                     WHERE username = @username";

            return await db.QueryFirstOrDefaultAsync<AssociateDto>(query, new { username });
        }

        public async Task InsertAsync(AssociateDto model)
        {
            using var db = CreateConnection();

            string query = @"INSERT INTO robox_m_associate
                    (associate_name,
                     associate_name_2,
                     associate_name_3,
                     availability,
                     active,
                     username,
                     rec_created)
                     VALUES
                    (@AssociateName,
                     @AssociateName2,
                     @AssociateName3,
                     @Availability,
                     @Active,
                     @Username,
                     GETDATE())";

            await db.ExecuteAsync(query, model);
        }

        public async Task DeactivateByUsernameAsync(string username)
        {
            using var db = CreateConnection();

            string query = @"UPDATE robox_m_associate
                     SET active = 0,
                         last_modified = GETDATE()
                     WHERE username = @username";

            await db.ExecuteAsync(query, new { username });
        }

        public async Task ReactivateAsync(string username)
        {
            using var db = CreateConnection();

            string query = @"UPDATE robox_m_associate
                     SET active = 1,
                         last_modified = GETDATE()
                     WHERE username = @username";

            await db.ExecuteAsync(query, new { username });
        }


    }

}
