using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ComplaintManagement.Util.Models.Incident
{
    public class IncidentCopyViewModel
    {
        public int ParentTicketId { get; set; }

        public int Category { get; set; }

        public int ServiceType { get; set; }

        public int ServiceItem { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string? Contact { get; set; }

        public string? AssignedTo { get; set; }

        public string? RelationType { get; set; }

        public List<SelectListItem>? CategoryList { get; set; }
        public List<SelectListItem>? ServiceTypeList { get; set; }
        public List<SelectListItem>? ServiceList { get; set; }
    }
}
