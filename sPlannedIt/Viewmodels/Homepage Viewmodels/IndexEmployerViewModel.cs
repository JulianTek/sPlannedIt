using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sPlannedIt.Interface;
using sPlannedIt.Logic.Models;

namespace sPlannedIt.Viewmodels.Homepage_Viewmodels
{
    public class IndexEmployerViewModel
    {
        public string CompanyID { get; set; }

        public List<Schedule> Schedules { get; set; }
        public List<IShift> TodaysWorkers { get; set; }
    }
}
