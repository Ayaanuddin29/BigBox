using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Master
{
    public class SubCategoryModel
    {
        public int? Id { get; set; }

        [JsonProperty("category_id")]
        public int CategoryId { get; set; }

        public string Name { get; set; }

        [JsonProperty("name_ar")]
        public string? Name_Ar { get; set; }

        public bool? Active { get; set; } = true;

        public string? login_created { get; set; }
    }

}
