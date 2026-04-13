using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.SLM
{
    public class SltDto
    {
        public int SlaId { get; set; }
        public string? SlaName { get; set; }
        public string? SlaNameAr { get; set; }
        public string? SlaNameOth { get; set; }
        public string? Details { get; set; }

        public int? ExpectedInitialResponseDays { get; set; }
        public int? ExpectedInitialResponseHours { get; set; }
        public int? ExpectedInitialResponseMinutes { get; set; }

        public int? ExpectedCloseDays { get; set; }
        public int? ExpectedCloseHours { get; set; }
        public int? ExpectedCloseMinutes { get; set; }

        public int? SlaThreatenDays { get; set; }
        public int? SlaThreatenHours { get; set; }
        public int? SlaThreatenMinutes { get; set; }

        public bool? Active { get; set; }
        public int? SlaTypeId { get; set; }

        public DateTime? RecCreated { get; set; }
        public string? LoginCreated { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LoginModified { get; set; }
    }
}
