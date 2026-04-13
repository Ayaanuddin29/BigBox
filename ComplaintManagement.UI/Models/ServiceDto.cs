using Newtonsoft.Json;

namespace ComplaintManagement.UI.Models
{
    public class ServiceDto
    {
        public int? Id { get; set; }
        //[JsonProperty("sub_category_id")]
        public int SubCategoryId { get; set; }
        public string? Name { get; set; }

        [JsonProperty("name_ar")]
        public string? Name_Ar { get; set; }

        public bool? Active { get; set; } = true;
        public string? login_created { get; set; }
    }
}
