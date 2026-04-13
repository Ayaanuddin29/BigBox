using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Dashboard
{
    public class DashboardSummaryDto
    {
        public int TotalTickets { get; set; }
        public List<StatusCountDto> StatusCounts { get; set; }
        public List<PriorityCountDto> PriorityCounts { get; set; }
        public List<CategoryCountDto> CategoryCounts { get; set; }
        public List<DailyCountDto> DailyCounts { get; set; }
    }

    public class StatusCountDto
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }

    public class PriorityCountDto
    {
        public string Priority { get; set; }
        public int Count { get; set; }
    }

    public class CategoryCountDto
    {
        public string Category { get; set; }
        public int Count { get; set; }
    }

    public class DailyCountDto
    {
        public DateTime rec_created { get; set; }
        public int Count { get; set; }
    }
}
