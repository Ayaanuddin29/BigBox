using ComplaintManagement.Business.Interfaces.Dashboard;
using ComplaintManagement.Data.Interfaces.Dashboard;
using ComplaintManagement.Util.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repo;

        public DashboardService(IDashboardRepository repo)
        {
            _repo = repo;
        }

        public async Task<DashboardSummaryDto> GetDashboardAsync()
        {
            return await _repo.GetDashboardSummaryAsync();
        }
    }
}
