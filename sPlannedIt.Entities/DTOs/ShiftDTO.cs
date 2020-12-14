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

        public string ShiftId { get; set; }
        public string ScheduleId { get; set; }
        public string UserId { get; set; }
        public DateTime ShiftDate { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    }
}
