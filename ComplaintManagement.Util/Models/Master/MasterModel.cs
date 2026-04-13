using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Master
{
    public class MasterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Name_ar { get; set; }
        public string? Name_oth { get; set; }
        public bool? Active { get; set; }
        public string TableName { get; set; }  // Example: "robox_m_category"
        public string? login_created { get; set; }
        public DateTime? rec_created { get; set; }
    }
}
