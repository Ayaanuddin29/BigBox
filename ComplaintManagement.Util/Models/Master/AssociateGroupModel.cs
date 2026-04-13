using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Master
{
    public class AssociateGroupModel
    {
        public int? associate_group_id { get; set; }
        public string group_name { get; set; }
       
        public string? group_name_2 { get; set; }
        public bool? Active { get; set; } = true;
        public string? login_created { get; set; }
    }
}
