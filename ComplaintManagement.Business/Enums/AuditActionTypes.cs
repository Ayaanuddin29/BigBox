using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintManagement.Business.Enums
{
    public static class AuditActionTypes
    {
        public const string IncidentCreated = "IncidentCreated";
        public const string StatusChanged = "StatusChanged";
        public const string AssignmentChanged = "AssignmentChanged";
        public const string PriorityChanged = "PriorityChanged";
        public const string FieldUpdated = "FieldUpdated";
    }
}
