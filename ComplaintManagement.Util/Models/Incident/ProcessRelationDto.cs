using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Incident
{
    public class ProcessRelationDto
    {
        public int RelationId { get; set; }

        public int ParentId { get; set; }

        public int ChildId { get; set; }

        public int ParentModule { get; set; }

        public int ChildModule { get; set; }

        public bool AllowUpdates { get; set; }

        public int RelationTypeId { get; set; }

        public int? Comp { get; set; }

        public DateTime RecCreated { get; set; }

        public string LoginCreated { get; set; }
    }
}
