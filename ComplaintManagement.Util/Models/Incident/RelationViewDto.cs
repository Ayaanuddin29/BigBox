using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Incident
{
    public class RelationViewDto
    {
        public int ParentId { get; set; }

        public int ChildId { get; set; }

        public string RelationType { get; set; }
    }
}
