using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sPlannedIt.Viewmodels.Schedule_Viewmodels
{
    public class CreateShiftViewmodel
    {
        public string ShiftId { get; set; }
        public string ScheduleId { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
