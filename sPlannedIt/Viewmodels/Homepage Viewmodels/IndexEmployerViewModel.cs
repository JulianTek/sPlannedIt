using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sPlannedIt.Entities.Models;
using sPlannedIt.Interface;

namespace sPlannedIt.Viewmodels.Homepage_Viewmodels
{
    public class IndexEmployerViewModel
    {
        public string CompanyID { get; set; }

        public List<Schedule> Schedules { get; set; }
        public List<Shift> TodaysWorkers { get; set; }
    }
}
