using System;

namespace sPlannedIt.Interface
{
    public interface IShift
    {
        string ShiftID { get; set; }
        string ScheduleID { get; set; }
        string UserID { get; set; }
        DateTime ShiftDate { get; set; }
        int StartTime { get; set; }
        int EndTime { get; set; }
        IShift UpdateShift(string id, string userId, DateTime shiftDate, int startTime, int endTime);
    }
}