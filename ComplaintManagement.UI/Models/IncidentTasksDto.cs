using System.ComponentModel.DataAnnotations;

namespace ComplaintManagement.UI.Models
{
    public class IncidentTasksDto
    {
        public int? Incid { get; set; }       

        public int? ObjectId { get; set; }
        public int? OrderType { get; set; }
        public int? SequenceId { get; set; }
        public int? TaskAddedBy { get; set; }
        public int? TaskRequestedBy { get; set; }
        public int? TaskType { get; set; }
        public int? TaskMode { get; set; }
        public int? ServiceItemTaskId { get; set; }

        public string TaskSummary { get; set; }

        public string TaskDetails1 { get; set; }

        public string? TaskDetails2 { get; set; }

        public string? ActionNotes { get; set; }

        public DateTime? TaskCreatedOn { get; set; }
        public int? AssignedToAssociate { get; set; }
        public int? AssignedToGroup { get; set; }
        public DateTime? TaskReqStartDate { get; set; }
        public DateTime? TaskReqEndDate { get; set; }
        public DateTime? ScheduledStartDate { get; set; }
        public DateTime? ScheduledEndDate { get; set; }
        public DateTime? TaskActualStartDate { get; set; }
        public DateTime? TaskActualEndDate { get; set; }

        public int? TotTimeSpendDays { get; set; }
        public int? TotTimeSpentHours { get; set; }
        public int? TotTimeSpentMin { get; set; }

        public string? TaskCurrency { get; set; }

        public decimal? TotalCost { get; set; }

        public int? TaskStatus { get; set; }
        public int? LastModify { get; set; }

       
        public int Comp { get; set; }

        public DateTime? RecCreated { get; set; }

        public string? LoginCreated { get; set; }

        public DateTime? LastModified { get; set; }

        public string? LoginModified { get; set; }
        public List<IFormFile>? Files { get; set; }

    }
}
