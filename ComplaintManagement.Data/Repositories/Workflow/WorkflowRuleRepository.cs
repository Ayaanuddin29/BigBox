using ComplaintManagement.Data.Interfaces.Workflow;
using ComplaintManagement.Util.Models.Workflow;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Repositories.Workflow
{
    public class WorkflowRuleRepository : IWorkflowRuleRepository
    {
        private readonly string _connectionString;

        public WorkflowRuleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public async Task<IEnumerable<WorkflowRuleDto>> GetAllAsync()
        {
            using var db = CreateConnection();
            return await db.QueryAsync<WorkflowRuleDto>(
                "dbo.Proc_WorkflowRule_GetAll",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<WorkflowRuleDto?> GetByIdAsync(int id)
        {
            using var db = CreateConnection();
            return await db.QueryFirstOrDefaultAsync<WorkflowRuleDto>(
                "dbo.Proc_WorkflowRule_GetById",
                new { RuleId = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> InsertAsync(WorkflowRuleDto model, string loginCreated)
        {
            using var db = CreateConnection();

            var p = new DynamicParameters();
            p.Add("@RuleNumber", model.RuleNumber);
            p.Add("@RuleName", model.RuleName);
            p.Add("@OrderTypeId", model.OrderTypeId);
            p.Add("@CategoryId", model.CategoryId);
            p.Add("@ServiceTypeId", model.ServiceTypeId);
            p.Add("@ServiceItemId", model.ServiceItemId);
            p.Add("@WorkflowUnitId", model.WorkflowUnitId);
            p.Add("@LanguageId", model.LanguageId);
            p.Add("@PriorityId", model.PriorityId);
            p.Add("@SltHours", model.SltHours);
            p.Add("@AssignToAssociateId", model.AssignToAssociateId);
            p.Add("@AssignToGroupId", model.AssignToGroupId);
            p.Add("@AssociateTypeId", model.AssociateTypeId);           
            p.Add("@LoginCreated", loginCreated);

            var newId = await db.ExecuteScalarAsync<int>(
                "dbo.Proc_WorkflowRule_Insert",
                p,
                commandType: CommandType.StoredProcedure);

            return newId;
        }

        public async Task<bool> UpdateAsync(WorkflowRuleDto model, string loginModified)
        {
            using var db = CreateConnection();

            var p = new DynamicParameters();          
            p.Add("@RuleNumber", model.RuleNumber);
            p.Add("@RuleName", model.RuleName);
            p.Add("@OrderTypeId", model.OrderTypeId);
            p.Add("@CategoryId", model.CategoryId);
            p.Add("@ServiceTypeId", model.ServiceTypeId);
            p.Add("@ServiceItemId", model.ServiceItemId);
            p.Add("@WorkflowUnitId", model.WorkflowUnitId);
            p.Add("@LanguageId", model.LanguageId);
            p.Add("@PriorityId", model.PriorityId);
            p.Add("@SltHours", model.SltHours);           
            p.Add("@AssignToAssociateId", model.AssignToAssociateId);
            p.Add("@AssignToGroupId", model.AssignToGroupId);
            p.Add("@AssociateTypeId", model.AssociateTypeId);
            p.Add("@LoginModified", loginModified);

            var rows = await db.ExecuteAsync(
                "dbo.Proc_WorkflowRule_Update",
                p,
                commandType: CommandType.StoredProcedure);

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var db = CreateConnection();
            var rows = await db.ExecuteAsync(
                "dbo.Proc_WorkflowRule_Delete",
                new { RuleId = id },
                commandType: CommandType.StoredProcedure);
            return rows > 0;
        }
    }
}
