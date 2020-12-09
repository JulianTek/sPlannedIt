using System;
using System.Collections.Generic;
using System.Text;

namespace sPlannedIt.Entities.DTOs
{
    public class ShiftDTO
    {
        public ShiftDTO()
        {

        }

        public ShiftDTO(string shiftId, string scheduleId, string userId, DateTime shiftDate, int startTime, int endTime)
        {
            ShiftId = shiftId;
            ScheduleId = scheduleId;
            UserId = userId;
            ShiftDate = shiftDate;
            StartTime = startTime;
            EndTime = endTime;
        }

        public string ShiftId { get; private set; }
        public string ScheduleId { get; private set; }
        public string UserId { get; set; }
        public DateTime ShiftDate { get; private set; }
        public int StartTime { get; private set; }
        public int EndTime { get; private set; }
    }
}
