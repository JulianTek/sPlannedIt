using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace sPlannedIt.Models
{
    public class Shift
    {
        public Shift(string userId, DateTime shiftDate, int startTime, int endTime)
        {
            ShiftID = new Guid().ToString();
            UserID = userId;
            ShiftDate = shiftDate;
            StartTime = startTime;
            EndTime = endTime;
        }

        public string ShiftID { get; set; }
        public string ScheduleID { get; set; }
        public string UserID { get; set; }
        public DateTime ShiftDate { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }


        public Shift UpdateShift(string id, string userId, DateTime shiftDate, int startTime, int endTime)
        {
            UserID = userId;
            ShiftDate = shiftDate;
            StartTime = startTime;
            EndTime = endTime;
            return this;
        }
    }
}
