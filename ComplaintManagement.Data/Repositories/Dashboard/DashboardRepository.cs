using ComplaintManagement.Data.Interfaces.Dashboard;
using ComplaintManagement.Util.Models.Dashboard;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Repositories.Dashboard
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IDbConnection _db;

        public DashboardRepository(IConfiguration config)
        {
            _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
        {
            using var multi = await _db.QueryMultipleAsync(
                "ProcGet_Incident_DashboardSummary",
                commandType: CommandType.StoredProcedure);

            var total = await multi.ReadFirstAsync<int>();
            var status = (await multi.ReadAsync<StatusCountDto>()).ToList();
            var priority = (await multi.ReadAsync<PriorityCountDto>()).ToList();
            var category = (await multi.ReadAsync<CategoryCountDto>()).ToList();
            var daily = (await multi.ReadAsync<DailyCountDto>()).ToList();

            return new DashboardSummaryDto
            {
                TotalTickets = total,
                StatusCounts = status,
                PriorityCounts = priority,
                CategoryCounts = category,
                DailyCounts = daily
            };
        }
    }
}
