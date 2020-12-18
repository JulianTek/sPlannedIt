using System;
using System.Collections.Generic;
using sPlannedIt.Entities.Models;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Interface.BLL
{
    public interface IScheduleCollection : IHandler<Schedule>
    {
        List<Shift> GetShiftsFromSchedule(string id);
        List<Shift> GetTodaysShifts(string id, DateTime date);
        List<Schedule> GetSchedulesFromCompany(string id);
    }
}
