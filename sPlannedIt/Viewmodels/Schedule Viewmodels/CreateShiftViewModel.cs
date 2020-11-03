using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sPlannedIt.Viewmodels.Schedule_Viewmodels
{
    public class CreateShiftViewModel
    {
        public string ShiftId { get; set; }
        public string ScheduleId { get; set; }
        // Temporary: will eventually make a select from all employees
        public string UserEmail { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        
    }
}
