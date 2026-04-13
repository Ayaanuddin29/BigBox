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
    public class CategoryRepository :ICategoryRepository
    {
        private readonly IDbConnection _db;

        public CategoryRepository(IConfiguration config)
        {
            _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            string query = "SELECT Id, Name, Name_Ar, Active FROM robox_m_category ORDER BY Id DESC";
            return await _db.QueryAsync<CategoryModel>(query);
        }

        public async Task<int> CreateAsync(CategoryModel model)
        {
            string query = @"INSERT INTO robox_m_category (Name, Name_Ar, Active, login_created)
                             VALUES (@Name, @Name_Ar, 1, @login_created)";
            return await _db.ExecuteAsync(query, model);
        }

        public async Task<int> UpdateAsync(CategoryModel model)
        {
            string query = @"UPDATE robox_m_category SET Name=@Name, Name_Ar=@Name_Ar, Active=@Active WHERE Id=@Id";
            return await _db.ExecuteAsync(query, model);
        }

        public async Task<int> DeleteAsync(int id)
        {
            string query = @"UPDATE robox_m_category SET Active=0 WHERE Id=@Id";
            return await _db.ExecuteAsync(query, new { Id = id });
        }

    }
}
