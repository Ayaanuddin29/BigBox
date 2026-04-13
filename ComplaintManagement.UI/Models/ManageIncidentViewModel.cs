using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComplaintManagement.UI.Models
{
    public class ManageIncidentViewModel
    {
        public string Number { get; set; }
        public string Caller { get; set; }
        public int Category { get; set; }
        public int SubCategory { get; set; }

        public int Service { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string Impact { get; set; }
        public string Urgency { get; set; }
        public string Priority { get; set; }
        public string AssignedTo { get; set; }

        [ValidateNever]   // prevents model validation on this property
        public List<SelectListItem> Categorys { get; set; }
        [ValidateNever]   // prevents model validation on this property

        public List<SelectListItem> SubCategorys { get; set; }
        [ValidateNever]   // prevents model validation on this property

        public List<SelectListItem> Services { get; set; }
        [ValidateNever]   // prevents model validation on this property

        public List<SelectListItem> Impacts { get; set; }
        [ValidateNever]   // prevents model validation on this property

        public List<SelectListItem> Urgencys { get; set; }
        [ValidateNever]   // prevents model validation on this property

        public List<SelectListItem> Prioritys { get; set; }
        [ValidateNever]   // prevents model validation on this property
        public List<SelectListItem> States { get; set; }

        public bool MajorIncident { get; set; }


    }
}
