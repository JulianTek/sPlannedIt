using System.Collections.Generic;

namespace sPlannedIt.Interface
{
    public interface ISchedule
    {
        string ScheduleID { get; set; }
        string CompanyID { get; set; }
        List<IShift> Shifts { get; set; }
        bool AddShift(IShift shift);
        bool RemoveShift(IShift shift);
        ISchedule UpdateSchedule(string companyID, string name);
    }
}