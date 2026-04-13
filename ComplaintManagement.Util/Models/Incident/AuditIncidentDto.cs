using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Incident
{
    public class AuditIncidentDto
    {
        public int Id { get; set; }
        public int Incid_No { get; set; }
        public string ActionType { get; set; }
        public string Module { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Login_Created { get; set; }
        public DateTime Rec_Created { get; set; }
    }
}
