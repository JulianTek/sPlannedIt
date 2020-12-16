using System;

namespace sPlannedIt.Entities.Models
{
    public class Shift 
    {

        public Shift(string shiftId, string scheduleId, string userId, DateTime shiftDate, int startTime, int endTime)
        {
            ShiftId = shiftId;
            ScheduleId = scheduleId;
            UserId = userId;
            ShiftDate = shiftDate;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Shift()
        {
            
        }

        public string ShiftId { get; private set; }
        public string ScheduleId { get; private set; }
        public string UserId { get; private set; }
        public DateTime ShiftDate { get; private set; }
        public int StartTime { get; private set; }
        public int EndTime { get; private set; }
    }
}
