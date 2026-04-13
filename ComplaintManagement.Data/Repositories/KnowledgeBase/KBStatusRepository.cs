using ComplaintManagement.Data.Interfaces.KnowledgeBase;
using ComplaintManagement.Util.Models.KnowledgeBase;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ComplaintManagement.Data.Repositories.KnowledgeBase
{
    public class KBStatusRepository : IKBStatusRepository
    {
        private readonly IDbConnection _db;

        public KBStatusRepository(IConfiguration config)
        {
            _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<KBStatusModel>> GetAllAsync()
        {
            string query = "SELECT Id, Name, Description FROM robox_m_KB_Status ORDER BY Id DESC";
            return await _db.QueryAsync<KBStatusModel>(query);
        }

        public async Task<int> CreateAsync(KBStatusModel model)
        {
            string query = @"INSERT INTO robox_m_KB_Status (Name, Description)
                             VALUES (@Name, @Description)";

            return await _db.ExecuteAsync(query, model);
        }

        public async Task<int> UpdateAsync(KBStatusModel model)
        {
            string query = @"UPDATE robox_m_KB_Status
                             SET Name=@Name, Description=@Description
                             WHERE Id=@Id";

            return await _db.ExecuteAsync(query, model);
        }

        public async Task<int> DeleteAsync(int id)
        {
            string query = @"DELETE FROM robox_m_KB_Status WHERE Id=@Id";

            return await _db.ExecuteAsync(query, new { Id = id });
        }
    }
}