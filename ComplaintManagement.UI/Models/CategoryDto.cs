using Newtonsoft.Json;

namespace ComplaintManagement.UI.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("name_ar")]
        public string Name_Ar { get; set; } // optional
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } // optional
    }
}
