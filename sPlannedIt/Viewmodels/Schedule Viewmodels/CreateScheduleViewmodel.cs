using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sPlannedIt.Logic.Models;

namespace sPlannedIt.Viewmodels.Schedule_Viewmodels
{
    public class CreateScheduleViewmodel
    {
        public string ShiftId { get; set; }
        public string CompanyId { get; set; }
        public List<Shift> Shifts { get; set; }
    }
}
