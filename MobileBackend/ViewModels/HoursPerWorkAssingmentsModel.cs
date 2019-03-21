using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileBackend.ViewModels
{
    public class HoursPerWorkAssignmentsModel
    {
        public int WorkAssignmentId { get; set; }
        public string WorkAssigmentName { get; set; }
        public double TotalHours { get; set; }

    }
}