using ComplaintManagement.Util.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Data.Interfaces.Dashboard
{
    public interface IDashboardRepository
    {
        Task<DashboardSummaryDto> GetDashboardSummaryAsync();
    }
}
