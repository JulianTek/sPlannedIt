using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sPlannedIt.Interface;
using sPlannedIt.Logic.Models;
using sPlannedIt.Models;

namespace sPlannedIt.Viewmodels.Homepage_Viewmodels
{
    public class IndexEmployeeViewModel
    {
        public string CompanyID { get; set; }
        public List<Shift> Shifts { get; set; }
        public List<Shift> TodaysWorkers { get; set; }
    }
}
