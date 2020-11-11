using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Entities.DTOs;

namespace sPlannedIt.Interface.DAL
{
    public interface IScheduleHandler : IHandler<ScheduleDTO>
    {
        public List<ShiftDTO> GetShiftsFromSchedule(string id);
    }
}
