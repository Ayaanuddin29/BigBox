using ComplaintManagement.Data.Interfaces.SLM;
using ComplaintManagement.Util.Models.SLM;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Repositories.SLM
{
    public class SltRepository : ISltRepository
    {
        private readonly string _conn;

        public SltRepository(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Conn => new SqlConnection(_conn);

        public async Task<IEnumerable<SltDto>> GetAllAsync()
        {
            return await Conn.QueryAsync<SltDto>(
                "Proc_SLT_GetAll",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<SltDto?> GetByIdAsync(int id)
        {
            return await Conn.QueryFirstOrDefaultAsync<SltDto>(
                "Proc_SLT_GetById",
                new { Sla_Id = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> InsertAsync(SltDto model, string login)
        {
            var param = new DynamicParameters();
            //param.Add("@LoginCreated", login);
            param.Add("@sla_name", model.SlaName);
            param.Add("@sla_name_ar", model.SlaNameAr);
            param.Add("@sla_name_oth", model.SlaNameOth);
            param.Add("@details", model.Details);
            param.Add("@expected_initial_response_days", model.ExpectedInitialResponseDays);
            param.Add("@expected_initial_response_hours", model.ExpectedInitialResponseHours);
            param.Add("@expected_initial_response_minutes", model.ExpectedInitialResponseMinutes);
            param.Add("@expected_close_days", model.ExpectedCloseDays);
            param.Add("@expected_close_hours", model.ExpectedCloseHours);
            param.Add("@expected_close_minutes", model.ExpectedCloseMinutes);
            param.Add("@sla_threaten_days", model.SlaThreatenDays); 
            param.Add("@sla_threaten_hours", model.SlaThreatenHours);
            param.Add("@sla_threaten_minutes", model.SlaThreatenMinutes);
            param.Add("@sla_type_id", model.SlaTypeId);
            param.Add("@active", model.Active);
            param.Add("@rec_created", model.RecCreated);            
            param.Add("@login_created", model.LoginCreated);

            return await Conn.ExecuteScalarAsync<int>(
                "dbo.Proc_SLT_Insert",
                param,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateAsync(SltDto model, string login)
        {
            var param = new DynamicParameters();
            //param.Add("@LoginModified", login);
            param.Add("@sla_id", model.SlaId);
            param.Add("@sla_name", model.SlaName);
            param.Add("@sla_name_ar", model.SlaNameAr);
            param.Add("@sla_name_oth", model.SlaNameOth);
            param.Add("@details", model.Details);
            param.Add("@expected_initial_response_days", model.ExpectedInitialResponseDays);
            param.Add("@expected_initial_response_hours", model.ExpectedInitialResponseHours);
            param.Add("@expected_initial_response_minutes", model.ExpectedInitialResponseMinutes);
            param.Add("@expected_close_days", model.ExpectedCloseDays);
            param.Add("@expected_close_hours", model.ExpectedCloseHours);
            param.Add("@expected_close_minutes", model.ExpectedCloseMinutes);
            param.Add("@sla_threaten_days", model.SlaThreatenDays);
            param.Add("@sla_threaten_hours", model.SlaThreatenHours);
            param.Add("@sla_threaten_minutes", model.SlaThreatenMinutes);
            param.Add("@sla_type_id", model.SlaTypeId);
            param.Add("@active", model.Active);
            param.Add("@last_modified", model.LastModified);
            param.Add("@login_modified", model.LoginModified);

            int result = await Conn.ExecuteAsync(
                "Proc_SLT_Update",
                param,
                commandType: CommandType.StoredProcedure);

            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            int result = await Conn.ExecuteAsync(
                "Proc_SLT_Delete",
                new { Sla_Id = id },
                commandType: CommandType.StoredProcedure);

            return result > 0;
        }
    }
}
