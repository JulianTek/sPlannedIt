using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sPlannedIt.Interface;
using sPlannedIt.Logic.Models;

namespace sPlannedIt.Viewmodels.Schedule_Viewmodels
{
    public class CreateScheduleViewmodel
    {
        public string ScheduleId { get; set; }
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public List<Shift> Shifts { get; set; }
    }
}
