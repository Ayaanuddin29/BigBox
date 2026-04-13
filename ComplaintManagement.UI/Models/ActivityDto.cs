using System.ComponentModel.DataAnnotations;

namespace ComplaintManagement.UI.Models
{
    public class ActivityDto
    {
        public int Incid { get; set; }
        public string? Activity_Type { get; set; }
       
        public string? Activity_AddedBy { get; set; }
        [Required(ErrorMessage = "Summary is required")]
        public string Summary { get; set; }
       
        public string? Details { get; set; } 
        public string ?comment{get; set;}

        public int ?Time_Days { get; set; }
        public int? Time_Hours { get; set; }
        public int? Time_Minutes { get; set; }

        public DateTime ? Rec_Created { get; set; }
        public string? Login_Created { get; set; }
        public int ?Activity_Id { get; set; }

        // Files chosen in the form
        //public List<IFormFile> Files { get; set; } = new List<IFormFile>();
        public List<IFormFile> ? Files { get; set; }



    }
}
