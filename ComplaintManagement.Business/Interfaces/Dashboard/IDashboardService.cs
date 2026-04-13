using ComplaintManagement.Util.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Interfaces.Dashboard
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetDashboardAsync();
    }
}
