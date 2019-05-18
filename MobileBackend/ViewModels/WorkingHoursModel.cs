using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileBackend.ViewModels
{
    public class WorkingHoursModel
    {

        public int WorkerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Active { get; set; }

    }
}