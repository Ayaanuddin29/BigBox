using Newtonsoft.Json;

namespace ComplaintManagement.UI.Models
{
    public class SubCategoryDto
    {
        public int Id { get; set; }

        [JsonProperty("category_id")]
        public int CategoryId { get; set; }

        public string Name { get; set; }

        [JsonProperty("name_ar")]
        public string Name_Ar { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }
    }
}
