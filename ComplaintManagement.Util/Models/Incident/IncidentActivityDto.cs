using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Util.Models.Incident
{
    public class IncidentActivityDto
    {
        public int Incid { get; set; }
        public string? Activity_Type { get; set; }

        public string? Activity_AddedBy { get; set; }
       
        public string Summary { get; set; }
        
        public string? Details { get; set; }
        public string? comment { get; set; }

        public int? Time_Days { get; set; }
        public int? Time_Hours { get; set; }
        public int? Time_Minutes { get; set; }

        public DateTime? Rec_Created { get; set; }
        public string? Login_Created { get; set; }
        public int Activity_Id { get; set; }
        //public List<IFormFile> Files { get; set; } = new List<IFormFile>();
        public List<IFormFile> ?Files { get; set; } 

    }
}
