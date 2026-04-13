using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Incident
{
    public class ResolutionModel
    {
        public int resol_id { get; set; }
        public int incid_no { get; set; }

        public string summary { get; set; }
        public string details { get; set; }

        public int status { get; set; } // 1=Resolution, 2=Recovery

        public bool? solution_accept_or_reject { get; set; } // null / true / false
        public DateTime? accepted_or_rejected_on { get; set; }

        public string reason_for_rejection { get; set; }

        public bool active { get; set; }

        public DateTime rec_created { get; set; }

        public List<ResolutionAttachment>? Attachments { get; set; }
    }

    public class ResolutionAttachment
    {
        public int attach_id { get; set; }
        public int resol_id { get; set; }
        public string file_name { get; set; }
        public byte[] uploaded_file { get; set; }
    }
}
