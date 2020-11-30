using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Entities.DTOs;

namespace sPlannedIt.Interface.DAL
{
    public interface IScheduleHandler : IHandler<ScheduleDTO>
    {
        public List<ShiftDTO> GetShiftsFromSchedule(string id);
        public List<ShiftDTO> GetTodaysShifts(string id, DateTime date);
        public List<ScheduleDTO> GetSchedulesFromCompany(string id);
    }
}
