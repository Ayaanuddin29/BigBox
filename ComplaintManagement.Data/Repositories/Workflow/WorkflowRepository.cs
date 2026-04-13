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
    public class WorkflowRepository : IWorkflowRepository
{
    private readonly IDbConnection _db;

    public WorkflowRepository(IConfiguration config)
    {
        _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
    }

    public async Task<int> CreateAsync(WorkflowSaveModel model)
    {
       // using var tran = _db.BeginTransaction();

        try
        {
            int workflowId = await _db.ExecuteScalarAsync<int>(
                @"INSERT INTO robox_m_workflowmaster
                  (workflowname, module, login_created)
                  VALUES (@WorkflowName, @Module, @LoginCreated);
                  SELECT CAST(SCOPE_IDENTITY() AS INT);",
                model);

            await _db.ExecuteAsync(
                @"INSERT INTO robox_m_workflowdefinition
                  (workflowid, workflowjson, conditionjson)
                  VALUES (@WorkflowId, @WorkflowJson, @ConditionJson);",
                new
                {
                    WorkflowId = workflowId,
                    model.WorkflowJson,
                    model.ConditionJson
                }                );

            //tran.Commit();
            return workflowId;
        }
        catch
        {
            //tran.Rollback();
            throw;
        }
    }

    public async Task UpdateAsync(WorkflowSaveModel model)
    {
        using var tran = _db.BeginTransaction();

        await _db.ExecuteAsync(
            @"UPDATE robox_m_workflowmaster
              SET workflowname = @WorkflowName,
                  module = @Module
              WHERE workflowid = @WorkflowId",
            model, tran);

        await _db.ExecuteAsync(
            @"UPDATE robox_m_workflowdefinition
              SET workflowjson = @WorkflowJson,
                  conditionjson = @ConditionJson
              WHERE workflowid = @WorkflowId",
            model, tran);

        tran.Commit();
    }

    public async Task<WorkflowSaveModel> GetByIdAsync(int workflowId)
    {
        return await _db.QueryFirstOrDefaultAsync<WorkflowSaveModel>(
            @"SELECT m.workflowid,
                     m.workflowname,
                     m.module,
                     d.workflowjson,
                     d.conditionjson
              FROM robox_m_workflowmaster m
              JOIN robox_m_workflowdefinition d 
                ON m.workflowid = d.workflowid
              WHERE m.workflowid = @workflowId",
            new { workflowId });
    }

    public async Task<IEnumerable<WorkflowMasterModel>> GetAllAsync(int module)
    {
        return await _db.QueryAsync<WorkflowMasterModel>(
            @"SELECT workflowid, workflowname, module, active
              FROM robox_m_workflowmaster
              WHERE module = @module
              ORDER BY rec_created DESC",
            new { module });
    }
}

}
