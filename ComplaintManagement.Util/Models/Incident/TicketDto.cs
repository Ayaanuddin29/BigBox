using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Incident
{
    public class TicketDto
    {
        public int TicketId { get; set; }

        public string Service { get; set; }

        public string Status { get; set; }

        public string User { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
