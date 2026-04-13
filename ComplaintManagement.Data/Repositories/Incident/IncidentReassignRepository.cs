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
    public class IncidentReassignRepository : IIncidentReassignRepository
    {
        private readonly IDbConnection _db;

        public IncidentReassignRepository(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IncidentReassignModel> GetIncidentDetails(int incidentId)
        {
            string sql = @"SELECT incid_id IncidentId,
                       affected_userid Contact,
                       summary Summary,
                       priorityid PriorityId,
                       sltid SltId,
                       categoryid CategoryId,
                       subcategoryid TypeId,
                       serviceid ServiceId,
                       assigned_to_associate_id AssignedTo,
                       assigned_to_associate_id AssociateId
                       FROM robox_t_incidents
                       WHERE incid_id=@incidentId";

            return await _db.QueryFirstOrDefaultAsync<IncidentReassignModel>(sql, new { incidentId });
        }

        public async Task<bool> UpdateAssignment(IncidentReassignModel model)
        {
            string sql = @"UPDATE robox_t_incidents
                       SET assigned_to=@AssignedTo,
                       associate_id=@AssociateId
                       WHERE incident_id=@IncidentId";

            return await _db.ExecuteAsync(sql, model) > 0;
        }

        public async Task<bool> UpdatePrioritySlt(IncidentReassignModel model)
        {
            string sql = @"UPDATE robox_t_incidents
                       SET priorityid=@PriorityId,
                       sltid=@SltId
                       WHERE incident_id=@IncidentId";

            return await _db.ExecuteAsync(sql, model) > 0;
        }

        public async Task<bool> UpdateCategorization(IncidentReassignModel model)
        {
            string sql = @"UPDATE robox_t_incidents
                       SET categoryid=@CategoryId,
                       typeid=@TypeId,
                       serviceid=@ServiceId
                       WHERE incident_id=@IncidentId";

            return await _db.ExecuteAsync(sql, model) > 0;
        }
    }
}
