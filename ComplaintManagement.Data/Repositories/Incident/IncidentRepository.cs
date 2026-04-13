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
    public class IncidentRepository : IIncidentRepository
    {
        private readonly IDbConnection _db;

        public IncidentRepository(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<int> InsertIncidentAsync(IncidentCreateDto dto)
        {
            var p = new DynamicParameters();
            p.Add("@typeid", dto.TypeId, DbType.Int32);
            p.Add("@sourceid", dto.SourceId, DbType.Int32);
            p.Add("@affected_userid", dto.AffectedUserId, DbType.String);
            p.Add("@created_by_userid", dto.CreatedByUserId, DbType.String);
            p.Add("@phone", dto.Phone, DbType.String);
            p.Add("@mobile", dto.Mobile, DbType.String);
            p.Add("@classificationid", dto.ClassificationId, DbType.Int32);
            p.Add("@categoryid", dto.CategoryId, DbType.Int32);
            p.Add("@subcategoryid", dto.SubcategoryId, DbType.Int32);
            p.Add("@serviceid", dto.ServiceId, DbType.Int32);
            p.Add("@statusid", dto.StatusId, DbType.Int32);
            p.Add("@priorityid", dto.PriorityId, DbType.Int32);
            p.Add("@urgencyid", dto.UrgencyId, DbType.Int32);
            p.Add("@impactid", dto.ImpactId, DbType.Int32);
            p.Add("@summary", dto.Summary, DbType.String);
            p.Add("@description", dto.Description, DbType.String);
            p.Add("@assigned_by_associate_id", dto.AssignedByAssociateId, DbType.Int32);
            p.Add("@assigned_to_associate_id", dto.AssignedToAssociateId, DbType.Int32);
            p.Add("@assigned_to_group_id", dto.AssignedToGroupId, DbType.Int32);
            p.Add("@comp", dto.Comp, DbType.Int32);
            p.Add("@login_created", dto.LoginCreated, DbType.String);
            p.Add("@longitude", dto.Longitude, DbType.String);
            p.Add("@latitude", dto.Latitude, DbType.String);
            p.Add("@column1", dto.Column1, DbType.String);
            p.Add("@column2", dto.Column2, DbType.String);
            p.Add("@column3", dto.Column3, DbType.String);

            // output param
            p.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _db.ExecuteAsync(
                "dbo.ProcInsert_Incident_Create",
                p,
                commandType: CommandType.StoredProcedure);

            var newId = p.Get<int?>("@NewId") ?? 0;
            return newId;
        }

        public async Task<List<IncidentListDto>> GetIncidentListAsync()
        {
            var result = new List<IncidentListDto>();
            var connStr = _db.ConnectionString;

            using var conn = new SqlConnection(connStr);
            using var cmd = new SqlCommand("ProcGet_Incident_List", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new IncidentListDto
                {
                    Number = reader["Number"].ToString(),
                    Caller = reader["Caller"].ToString(),
                    Category = reader["Category"].ToString(),
                    ShortDescription = reader["ShortDescription"].ToString(),
                    State = reader["State"].ToString(),
                    Impact = reader["Impact"].ToString(),
                    Urgency = reader["Urgency"].ToString(),
                    Priority = reader["Priority"].ToString(),
                    AssignedTo = reader["AssignedTo"].ToString()
                });
            }

            return result;
        }

        public async Task<IncidentListDto> GetIncidentByIdAsync(string incId)
        {
            var result = new IncidentListDto();
            var connStr = _db.ConnectionString;

            using var conn = new SqlConnection(connStr);
            using var cmd = new SqlCommand("ProcGet_Incident_ById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            // Add input parameter
            cmd.Parameters.Add(new SqlParameter("@incid_id", SqlDbType.Int));
            cmd.Parameters["@incid_id"].Value = incId;


            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                result = new IncidentListDto
                {
                    Number = reader["Number"].ToString(),
                    Caller = reader["Caller"].ToString(),
                    Category = reader["Category"].ToString(),
                    SubCategory = reader["SubCategory"].ToString(),
                    Service = reader["Service"].ToString(),
                    ShortDescription = reader["ShortDescription"].ToString(),
                    State = reader["State"].ToString(),
                    Impact = reader["Impact"].ToString(),
                    Urgency = reader["Urgency"].ToString(),
                    Priority = reader["Priority"].ToString(),
                    AssignedTo = reader["AssignedTo"].ToString(),
                    Description = reader["Description"].ToString()


                };
            }

            return result;
        }

        public async Task<bool> UpdateIncidentAsync(IncidentUpdateDto dto)
        {
            var p = new DynamicParameters();

            p.Add("@IncidentId", dto.incidentId, DbType.Int32);
            p.Add("@categoryid", dto.CategoryId, DbType.Int32);
            p.Add("@subcategoryid", dto.SubcategoryId, DbType.Int32);
            //p.Add("@serviceid", dto.ServiceId, DbType.Int32);
            p.Add("@statusid", dto.StatusId, DbType.Int32);
            p.Add("@priorityid", dto.PriorityId, DbType.Int32);
            p.Add("@urgencyid", dto.UrgencyId, DbType.Int32);
            p.Add("@impactid", dto.ImpactId, DbType.Int32);
            p.Add("@summary", dto.Summary, DbType.String);
            p.Add("@description", dto.Description, DbType.String);
            p.Add("@assigned_to_associate_id", dto.AssignedToAssociateId, DbType.String);

            await _db.ExecuteAsync(
              "dbo.ProcUpdate_Incident_ById",
              p,
              commandType: CommandType.StoredProcedure);
            return true;
        }

        public async Task<int> CreateIncidentActivityAsync(IncidentActivityDto dto)
        {
            try
            {

                var p = new DynamicParameters();
                p.Add("@incid_no", dto.Incid, DbType.Int32);
                p.Add("@summary", dto.Summary, DbType.String);
                p.Add("@details", dto.Details, DbType.String);
                p.Add("@rec_created", DateTime.Now, DbType.DateTime);
                p.Add("@activity_type", 1, DbType.Int32);

                // output param
                p.Add("@activity_id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _db.ExecuteAsync(
                    "dbo.ProcInsert_Activity_Create",
                    p,
                    commandType: CommandType.StoredProcedure);

                var newId = p.Get<int?>("@activity_id") ?? 0;
                // Save each file via stored procedure
                if (dto.Files != null && dto.Files.Any())
                {
                    foreach (var file in dto.Files)
                    {
                        if (file.Length > 0)
                        {
                            using var ms = new MemoryStream();
                            await file.CopyToAsync(ms);
                            var fl = new DynamicParameters();
                            fl.Add("@file_name", file.FileName, DbType.String);
                            fl.Add("@uploaded_file", ms.ToArray(), DbType.Binary);
                            fl.Add("@activity_id", newId, DbType.Int32);
                            await _db.ExecuteAsync(
                                                    "dbo.ProcInsert_ActivityAttachment_Create",
                                                            fl,
                                                            commandType: CommandType.StoredProcedure);


                        }
                    }
                }

                return newId;
            }
            catch (Exception ex)
            {

            }
            return 0;
        }

        public async Task<List<ActivityReadDto>> GetActivitiesByIncIdAsync(string incId)
        {
            var activities = new Dictionary<int, ActivityReadDto>();

            //var result = new ActivityReadDto();
            var connStr = _db.ConnectionString;

            using var conn = new SqlConnection(connStr);
            using var cmd = new SqlCommand("ProcGet_IncidentActivity_ByIncId", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            // Add input parameter
            cmd.Parameters.Add(new SqlParameter("@incid_id", SqlDbType.Int));
            cmd.Parameters["@incid_id"].Value = incId;

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            var count = reader.HasRows;
            while (await reader.ReadAsync())
            {
                int activityId = reader.GetInt32(reader.GetOrdinal("ActivityId"));
                if (!activities.ContainsKey(activityId))
                {
                    activities[activityId] = new ActivityReadDto
                    {
                        activity_id = activityId,
                        Summary = reader["Summary"]?.ToString(),
                        Details = reader["Details"]?.ToString(),
                        Rec_created = reader["Rec_created"]?.ToString(),
                        Attachments = new List<ActivityAttachmentDto>()
                    };
                }

                // If FileName is NOT NULL, it's an attachment row
                if (reader["FileName"] != DBNull.Value)
                {
                    var attachment = new ActivityAttachmentDto
                    {
                        AttachmentId = reader.GetInt32(reader.GetOrdinal("AttachmentId")),
                        FileName = reader["FileName"].ToString(),
                        FileData = reader["UploadFile"] != DBNull.Value ? (byte[])reader["UploadFile"] : null,
                        //UploadedOn = reader["UploadedOn"] != DBNull.Value ? (DateTime)reader["UploadedOn"] : DateTime.MinValue
                    };
                    activities[activityId].Attachments.Add(attachment);
                }

            }
            return activities.Values.ToList();
        }

        public async Task<int> CreateIncidentTasksync(IncidentTasksDto dto)
        {
            try
            {

                var p = new DynamicParameters();
                p.Add("@Incid", dto.Incid, DbType.Int32);
                p.Add("@task_created_on", dto.TaskCreatedOn, DbType.DateTime);
                p.Add("@assigned_to_associate", dto.AssignedToAssociate.HasValue ? dto.AssignedToAssociate.Value : (object)DBNull.Value, DbType.Int32);
                p.Add("@assigned_to_group", dto.AssignedToGroup.HasValue ? dto.AssignedToGroup.Value : (object)DBNull.Value, DbType.Int32);
                p.Add("@rec_created", DateTime.Now, DbType.DateTime);
                p.Add("@task_type", 1, DbType.Int32);
                p.Add("@comp", 1, DbType.Int32);
                p.Add("@task_summary", dto.TaskSummary, DbType.String);
                p.Add("@task_details_1", dto.TaskDetails1, DbType.String);
                p.Add("@tot_time_spend_days", dto.TotTimeSpendDays, DbType.Int32);
                p.Add("@tot_time_spent_hours", dto.TotTimeSpentHours, DbType.Int32);
                p.Add("@tot_time_spent_min", dto.TotTimeSpentMin, DbType.Int32);
                p.Add("@task_status", dto.TaskStatus, DbType.Int32);
                p.Add("@task_req_start_date", dto.TaskReqStartDate, DbType.DateTime);
                p.Add("@task_req_end_date", dto.TaskReqEndDate, DbType.DateTime);
                p.Add("@task_status", dto.TaskType, DbType.Int32);
                // output param
                p.Add("@task_id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _db.ExecuteAsync(
                    "dbo.ProcInsert_IncidentTask_Create",
                    p,
                    commandType: CommandType.StoredProcedure);

                var newId = p.Get<int?>("@task_id") ?? 0;
                // Save each file via stored procedure
                if (dto.Files != null && dto.Files.Any())
                {
                    foreach (var file in dto.Files)
                    {
                        if (file.Length > 0)
                        {
                            using var ms = new MemoryStream();
                            await file.CopyToAsync(ms);
                            var fl = new DynamicParameters();
                            fl.Add("@file_name", file.FileName, DbType.String);
                            fl.Add("@uploaded_file", ms.ToArray(), DbType.Binary);
                            fl.Add("@task_id", newId, DbType.Int32);
                            await _db.ExecuteAsync(
                                                    "dbo.ProcInsert_TaskAttachment_Create",
                                                            fl,
                                                            commandType: CommandType.StoredProcedure);


                        }
                    }
                }

                return newId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

            //return 0;
        }
        public async Task<List<IncidentTaskReadDto>> GetTasksByIncIdAsync(string incId)
        {
            var activities = new Dictionary<int, IncidentTaskReadDto>();

            //var result = new ActivityReadDto();
            var connStr = _db.ConnectionString;

            using var conn = new SqlConnection(connStr);
            using var cmd = new SqlCommand("ProcGet_IncidentTask_ByIncId", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            // Add input parameter
            cmd.Parameters.Add(new SqlParameter("@incid_Id", SqlDbType.Int));
            cmd.Parameters["@incid_id"].Value = incId;

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            var count = reader.HasRows;
            while (await reader.ReadAsync())
            {
                int taskId = reader.GetInt32(reader.GetOrdinal("TaskId"));
                if (!activities.ContainsKey(taskId))
                {
                    activities[taskId] = new IncidentTaskReadDto
                    {
                        task_id = taskId,
                        TaskSummary = reader["Summary"]?.ToString(),
                        TaskDetails1 = reader["Details"]?.ToString(),
                        RecCreated = reader["Rec_created"] == DBNull.Value
                        ? (DateTime?)null
                        : Convert.ToDateTime(reader["Rec_created"]),
                        TaskCreatedOn = reader["Task_CreatedOn"] == DBNull.Value
                        ? (DateTime?)null
                        : Convert.ToDateTime(reader["Task_CreatedOn"]),
                        TaskStatus = reader["Status"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["Status"]),
                        Attachments = new List<TaskAttachmentDto>()
                    };
                }

                // If FileName is NOT NULL, it's an attachment row
                if (reader["FileName"] != DBNull.Value)
                {
                    var attachment = new TaskAttachmentDto
                    {
                        AttachmentId = reader.GetInt32(reader.GetOrdinal("AttachmentId")),
                        FileName = reader["FileName"].ToString(),
                        FileData = reader["UploadFile"] != DBNull.Value ? (byte[])reader["UploadFile"] : null,
                        //UploadedOn = reader["UploadedOn"] != DBNull.Value ? (DateTime)reader["UploadedOn"] : DateTime.MinValue
                    };
                    activities[taskId].Attachments.Add(attachment);
                }

            }
            return activities.Values.ToList();
        }

        public IncidentCopyViewModel GetTicketForCopy(int id)
        {
            var connStr = _db.ConnectionString;
            using var db = new SqlConnection(connStr);

            string sql = @"SELECT
                    incid_id AS ParentTicketId,
                    categoryid AS Category,
                    subcategoryid AS ServiceType,
                    serviceid AS ServiceItem,
                    summary AS Summary,
                    description AS Description,
                    affected_userid AS Contact,
                    1 AS AssignedTo              
                   FROM robox_t_incidents
                   WHERE incid_id = @id";

            return db.QueryFirstOrDefault<IncidentCopyViewModel>(sql, new { id }); //assigned_to_associate_id
        }

        public int CreateCopyTicket(IncidentCopyViewModel model)
        {
            var connStr = _db.ConnectionString;
            using var db = new SqlConnection(connStr);

            db.Open();

            using var tran = db.BeginTransaction();

            string insertIncident = @"

    INSERT INTO robox_t_incidents
    (
        categoryid,
        subcategoryid,
        serviceid,
        statusid,
        summary,
        description,
        affected_userid,        
        rec_created
    )

    VALUES
    (
        @Category,
        @ServiceType,
        @ServiceItem,
        1,
        @Summary,
        @Description,
        @Contact,        
        GETDATE()
    )

    SELECT CAST(SCOPE_IDENTITY() AS INT)
    ";

            int newTicketId = db.ExecuteScalar<int>(insertIncident, model, tran);   //assigned_to_associate_id, @AssignedTo,

            string relationSql = @"

    INSERT INTO dbo.robox_t_copy_ticket_reference
    (
        parent_id,
        child_id,
        parent_module,
        child_module,
        rec_created
    )

    VALUES
    (
        @ParentId,
        @ChildId,
        1,
        1,
        GETDATE()
    )";

            db.Execute(relationSql, new
            {
                ParentId = model.ParentTicketId,
                ChildId = newTicketId
            }, tran);

            tran.Commit();

            return newTicketId;
        }

        public async Task ToggleMajorIncident(string incidentNumber)
        {
            var connStr = _db.ConnectionString;
            using (var connection = new SqlConnection(connStr))
            {
                string sql = @"
            UPDATE robox_t_incidents
            SET major_incident = 
                CASE 
                    WHEN major_incident = 1 THEN 0
                    ELSE 1
                END
            WHERE incid_id = @incidentNumber";

                await connection.ExecuteAsync(sql, new { incidentNumber });
            }
        }

        public void UpdateState(string incidentId, string state)
        {
            using (var db = new SqlConnection(_db.ConnectionString))
            {
                var query = @"UPDATE robox_t_incidents 
                      SET statusid = @state 
                      WHERE incid_id = @incidentId";

                db.Execute(query, new { state = state, incidentId = incidentId });
            }
        }

        public async Task<int> SaveResolution(ResolutionModel model)
        {
            using (var con = new SqlConnection(_db.ConnectionString))
            {
                var query = @"
IF(@resol_id = 0)
BEGIN
    INSERT INTO robox_t_resolution
    (incid_no, summary, details, status, solution_accept_or_reject,
     accepted_or_rejected_on, reason_for_rejection, active, rec_created)
    VALUES
    (@incid_no, @summary, @details, @status, @solution_accept_or_reject,
     @accepted_or_rejected_on, @reason_for_rejection, 1, GETDATE());

    SELECT CAST(SCOPE_IDENTITY() as int);
END
ELSE
BEGIN
    UPDATE robox_t_resolution SET
        summary = @summary,
        details = @details,
        status = @status,
        solution_accept_or_reject = @solution_accept_or_reject,
        accepted_or_rejected_on = @accepted_or_rejected_on,
        reason_for_rejection = @reason_for_rejection,
        last_modified = GETDATE()
    WHERE resol_id = @resol_id;

    SELECT @resol_id;
END";

                var id = await con.ExecuteScalarAsync<int>(query, model);

                // 🔥 Update Incident Status
                var statusQuery = @"UPDATE robox_t_incidents 
                            SET statusid = 3
                            WHERE incid_id = @incid_no";

                await con.ExecuteAsync(statusQuery, new { model.incid_no });

                return id;
            }
        }

        public async Task SaveAttachment(ResolutionAttachment file)
        {
            using (var con = new SqlConnection(_db.ConnectionString))
            {
                var query = @"INSERT INTO robox_t_resolution_attachment
                      (file_name, uploaded_file, resol_id)
                      VALUES (@file_name, @uploaded_file, @resol_id)";

                await con.ExecuteAsync(query, file);
            }
        }

        public async Task<IEnumerable<ResolutionModel>> GetResolutions(int incid_no)
        {
            using (var con = new SqlConnection(_db.ConnectionString))
            {
                var query = @"SELECT * FROM robox_t_resolution
                      WHERE incid_no = @incid_no
                      ORDER BY rec_created DESC";

                return await con.QueryAsync<ResolutionModel>(query, new { incid_no });
            }
        }

        public async Task<ResolutionModel> GetResolutionById(int id)
        {
            using (var con = new SqlConnection(_db.ConnectionString))
            {
                var query = "SELECT * FROM robox_t_resolution WHERE resol_id=@id";
                return await con.QueryFirstOrDefaultAsync<ResolutionModel>(query, new { id });
            }
        }

    }
}