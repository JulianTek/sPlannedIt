using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sPlannedIt.Entities.Models;
using sPlannedIt.Interface;

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
