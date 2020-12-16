using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Entities.Models;

namespace sPlannedIt.Interface
{
    public interface IScheduleCollection
    {
        List<Schedule> GetAll();
        Schedule GetById(string id);
        Schedule Create(Schedule schedule);
        Schedule Update(Schedule schedule);
        bool Delete(string id);
        List<Shift> GetShiftsFromSchedule(string id);
        List<Shift> GetTodaysShifts(string id, DateTime date);
        List<Schedule> GetSchedulesFromCompany(string id);
    }
}
