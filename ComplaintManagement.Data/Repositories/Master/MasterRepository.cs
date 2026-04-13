using ComplaintManagement.Data.Interfaces.Master;
using ComplaintManagement.Util.Models.Master;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ComplaintManagement.Data.Repositories.Master
{
    public class MasterRepository:IMasterRepository
    {
        private readonly IDbConnection _db;

        public MasterRepository(IConfiguration config)
        {
            _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<MasterModel>> GetAllAsync(string tableName)
        {
            string query = $"SELECT Id, Name,Name_ar,Active FROM {tableName}";
            return await _db.QueryAsync<MasterModel>(query);
        }

        public async Task<int> InsertAsync(MasterModel model)
        {
            string query = $"INSERT INTO {model.TableName} (Name,Name_ar,Active, login_created) VALUES (@Name,@Name_ar,1, @login_created)";
            return await _db.ExecuteAsync(query, model);
        }

        public async Task<int> UpdateAsync(MasterModel model)
        {
            string query = $"UPDATE {model.TableName} SET Name = @Name, Name_ar = @Name_ar, Active = @Active WHERE Id = @Id";
            return await _db.ExecuteAsync(query, model);
        }

        public async Task<int> DeleteAsync(int id, string tableName)
        {
            string query = $"UPDATE {tableName} SET Active = 0 WHERE Id = @Id";
            return await _db.ExecuteAsync(query, new { Id = id });
        }

        public async Task<IEnumerable<AssociateModel>> GetAssociates()
        {
            string query = $"SELECT associate_id,associate_name,associate_name_2,Active FROM robox_m_associate";
            return await _db.QueryAsync<AssociateModel>(query);
        }

        public async Task<IEnumerable<AssociateGroupModel>> GetAssociateGroups()
        {
            string query = $"SELECT associate_group_id,group_name,group_name_2,Active FROM robox_m_associate_group";
            return await _db.QueryAsync<AssociateGroupModel>(query);
        }

    }
}
