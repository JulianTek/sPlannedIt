using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sPlannedIt.Entities.Models;

namespace sPlannedIt.Viewmodels.Schedule_Viewmodels
{
    public class ScheduleDetailsViewModel
    {
        public string Name { get; set; }
        public List<Shift> Shifts { get;  set; } = new List<Shift>();
    }
}
