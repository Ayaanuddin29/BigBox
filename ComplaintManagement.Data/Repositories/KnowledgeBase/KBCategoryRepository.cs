using ComplaintManagement.Data.Interfaces.KnowledgeBase;
using ComplaintManagement.Util.Models.KnowledgeBase;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ComplaintManagement.Data.Repositories.KnowledgeBase
{
    public class KBCategoryRepository : IKBCategoryRepository
    {
        private readonly IDbConnection _db;

        public KBCategoryRepository(IConfiguration config)
        {
            _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<KBCategoryModel>> GetAllAsync()
        {
            string query = "SELECT Id, Name, Description FROM robox_m_KB_Categories ORDER BY Id DESC";
            return await _db.QueryAsync<KBCategoryModel>(query);
        }

        public async Task<int> CreateAsync(KBCategoryModel model)
        {
            string query = @"INSERT INTO robox_m_KB_Categories (Name, Description)
                             VALUES (@Name, @Description)";
            return await _db.ExecuteAsync(query, model);
        }

        public async Task<int> UpdateAsync(KBCategoryModel model)
        {
            string query = @"UPDATE robox_m_KB_Categories
                             SET Name=@Name, Description=@Description
                             WHERE Id=@Id";
            return await _db.ExecuteAsync(query, model);
        }

        public async Task<int> DeleteAsync(int id)
        {
            string query = @"DELETE FROM robox_m_KB_Categories WHERE Id=@Id";
            return await _db.ExecuteAsync(query, new { Id = id });
        }
    }
}