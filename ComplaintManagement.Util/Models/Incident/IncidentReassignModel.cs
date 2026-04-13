using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Incident
{
    public class IncidentReassignModel
    {
        public int IncidentId { get; set; }

        public string Contact { get; set; }

        public string Summary { get; set; }

        public int PriorityId { get; set; }

        public int SltId { get; set; }

        public int CategoryId { get; set; }

        public int TypeId { get; set; }

        public int ServiceId { get; set; }

        public int AssignedTo { get; set; }

        public int AssociateId { get; set; }
    }
}
