using System;
using System.Collections.Generic;
using System.Text;

namespace sPlannedIt.Entities.DTOs
{
    public class ShiftDTO
    {
        public string ShiftID { get; set; }
        public string ScheduleID { get; set; }
        public string UserID { get; set; }
        public DateTime ShiftDate { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}
