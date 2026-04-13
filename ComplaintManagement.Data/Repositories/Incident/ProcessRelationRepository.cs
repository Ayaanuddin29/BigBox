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
    public class ProcessRelationRepository : IProcessRelationRepository
    {
        private readonly IDbConnection _db;

        public ProcessRelationRepository(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<TicketDto>> GetTickets()
        {
            string sql = @"SELECT incid_id  TicketId,
                              serviceid  Service,
                              statusid  status,
                              affected_userid  [User],
                              rec_created  CreationDate
                       FROM robox_t_incidents";

            return await _db.QueryAsync<TicketDto>(sql);
        }

        public async Task<int> AddRelation(List<ProcessRelationDto> relations)
        {
            string sql = @"INSERT INTO robox_t_process_relations
                      (parent_id,child_id,parent_module,child_module,
                       allow_updates,relation_type_id,comp,
                       rec_created,login_created)

                       VALUES (@ParentId,@ChildId,@ParentModule,
                               @ChildModule,@AllowUpdates,
                               @RelationTypeId,@Comp,
                               GETDATE(),@LoginCreated)";

            return await _db.ExecuteAsync(sql, relations);
        }

        public async Task<IEnumerable<RelationViewDto>> GetRelations(int parentId)
        {
            string sql = @"SELECT 
                        pr.parent_id ParentId,
                        pr.child_id ChildId,
                        rt.name RelationType
                      FROM robox_t_process_relations pr
                      JOIN robox_m_relation_type rt
                      ON pr.relation_type_id = rt.id
                      WHERE pr.parent_id = @parentId";

            return await _db.QueryAsync<RelationViewDto>(sql, new { parentId });
        }
    }
}
