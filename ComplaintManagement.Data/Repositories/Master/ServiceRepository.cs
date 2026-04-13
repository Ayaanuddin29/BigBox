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
    public class ServiceRepository : IServiceRepository
    {
        private readonly IDbConnection _db;

        public ServiceRepository(IConfiguration config)
        {
            _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<ServiceModel>> GetAllAsync()
        {
            string query = @"SELECT S.Id, S.Sub_Category_Id AS SubCategoryId , S.Name, S.Name_Ar, S.Active
                             FROM robox_m_service S
                             INNER JOIN robox_m_subcategory c ON s.Sub_Category_Id = c.Id
                             ORDER BY Id DESC";
            return await _db.QueryAsync<ServiceModel>(query);
        }
        public async Task<IEnumerable<ServiceModel>> GetBySubCategoryAsync(int subCategoryId)
        {
            string query = @"SELECT Id, Sub_Category_Id AS SubCategoryId , Name, Name_Ar, Active
                             FROM robox_m_service
                             WHERE Sub_Category_Id = @SubCategoryId
                             ORDER BY Id DESC";
            return await _db.QueryAsync<ServiceModel>(query, new { SubCategoryId = subCategoryId });
        }

        public async Task<int> CreateAsync(ServiceModel model)
        {
            string query = @"INSERT INTO robox_m_service (sub_category_id, Name, Name_Ar, Active, login_created)
                             VALUES (@SubCategoryId, @Name, @Name_Ar, 1, @login_created)";
            return await _db.ExecuteAsync(query, model);
        }

        public async Task<int> UpdateAsync(ServiceModel model)
        {
            string query = @"UPDATE robox_m_service
                             SET Name=@Name, Name_Ar=@Name_Ar, Active=@Active
                             WHERE Id=@Id";
            return await _db.ExecuteAsync(query, model);
        }

        public async Task<int> DeleteAsync(int id)
        {
            string query = @"UPDATE robox_m_service SET Active=0 WHERE Id=@Id";
            return await _db.ExecuteAsync(query, new { Id = id });
        }
    }
}
