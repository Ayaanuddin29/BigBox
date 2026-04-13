using ComplaintManagement.Data.Interfaces.Incident;
using ComplaintManagement.Util.Models.Incident;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Repositories.Incident
{
    public class AuditRepository : IAuditRepository
    {
        private readonly IDbConnection _db;

        public AuditRepository(IConfiguration config)
        {
            _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<int> InsertAsync(AuditIncidentDto model)
        {
            string sql = @"
        INSERT INTO robox_t_incident_audit
        (incid_no, actiontype, module, title, description, login_created, rec_created)
        VALUES
        (@Incid_No, @ActionType, @Module, @Title, @Description, @Login_Created, @Rec_Created);
        SELECT CAST(SCOPE_IDENTITY() as int);";

            return await _db.ExecuteScalarAsync<int>(sql, model);
        }

        public async Task<IEnumerable<AuditIncidentDto>> GetByIncidentAsync(int incidNo)
        {
            string sql = @"
        SELECT *
        FROM robox_t_incident_audit
        WHERE incid_no = @incidNo
        ORDER BY rec_created DESC";

            return await _db.QueryAsync<AuditIncidentDto>(sql, new { incidNo });
        }
    }
}
