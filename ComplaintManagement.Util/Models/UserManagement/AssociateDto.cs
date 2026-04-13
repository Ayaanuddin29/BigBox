using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.UserManagement
{
    public class AssociateDto
    {
        public int AssociateId { get; set; }
        public string AssociateName { get; set; }
        public string? AssociateName2 { get; set; }
        public string? AssociateName3 { get; set; }
        public bool Availability { get; set; }
        public string? AlternateAssociate { get; set; }
        public bool Active { get; set; }
        public string? Username { get; set; }
        public DateTime? RecCreated { get; set; }
        public DateTime? LastModified { get; set; }
    }

}
