using ComplaintManagement.Data.Interfaces.Master;
using ComplaintManagement.Util.Models.Master;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Repositories.Master
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly IDbConnection _db;

        public SubCategoryRepository(IConfiguration config)
        {
            _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<SubCategoryModel>> GetAllAsync()
        {
            string query = @"SELECT sc.Id, sc.Category_id, c.Name AS CategoryName, sc.Name, sc.Name_Ar, sc.Active
                             FROM robox_m_subcategory sc
                             INNER JOIN robox_m_category c ON sc.Category_id = c.Id
                             ORDER BY sc.Id DESC";
            return await _db.QueryAsync<SubCategoryModel>(query);
        }

        public async Task<IEnumerable<SubCategoryModel>> GetByCategoryAsync(int categoryId)
        {
            string query = @"SELECT Id, Category_id, Name, Name_Ar, Active 
                             FROM robox_m_subcategory
                             WHERE Category_id = @CategoryId
                             ORDER BY Id DESC";
            return await _db.QueryAsync<SubCategoryModel>(query, new { CategoryId = categoryId });
        }

        public async Task<int> CreateAsync(SubCategoryModel model)
        {
            string query = @"INSERT INTO robox_m_subcategory (Category_id, Name, Name_Ar, Active, login_created)
                             VALUES (@CategoryId, @Name, @Name_Ar, 1, @login_created)";
            return await _db.ExecuteAsync(query, model);
        }

        public async Task<int> UpdateAsync(SubCategoryModel model)
        {
            string query = @"UPDATE robox_m_subcategory 
                             SET Category_id=@CategoryId, Name=@Name, Name_Ar=@Name_Ar, Active=@Active 
                             WHERE Id=@Id";
            return await _db.ExecuteAsync(query, model);
        }

        public async Task<int> DeleteAsync(int id)
        {
            // 🔸 Soft delete (same as Category)
            string query = @"UPDATE robox_m_subcategory SET Active=0 WHERE Id=@Id";
            return await _db.ExecuteAsync(query, new { Id = id });
        }
    }
}
