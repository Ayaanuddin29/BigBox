using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Incident
{
    public class RelationRequest
    {
        public int ParentId { get; set; }

        public List<int> ChildIds { get; set; }

        public int RelationTypeId { get; set; }
    }
}
