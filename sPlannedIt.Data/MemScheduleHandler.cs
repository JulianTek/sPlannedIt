using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Data
{
    class MemScheduleHandler : IScheduleHandler
    {
        private readonly List<ScheduleDTO> _schedules = new List<ScheduleDTO>();
        public List<ScheduleDTO> GetAll()
        {
            return _schedules;
        }

        public bool Create(ScheduleDTO entity)
        {
            int oldCount = _schedules.Count;
            _schedules.Add(entity);
            if (_schedules.Count != oldCount)
            {
                return true;
            }

            return false;
        }

        public bool Update(ScheduleDTO entity)
        {
            ScheduleDTO sched = _schedules.FirstOrDefault(s => s.ScheduleId == entity.ScheduleId);
            if (sched != null)
            {
                sched.ScheduleId = entity.ScheduleId;
                sched.CompanyId = entity.CompanyId;
                sched.Name = entity.Name;
                return true;
            }

            return false;
        }

        public bool Delete(string id)
        {
            int oldCount = _schedules.Count;
            ScheduleDTO sched = _schedules.FirstOrDefault(s => s.ScheduleId == id);
            _schedules.Remove(sched);
            return oldCount != _schedules.Count;
        }

        public ScheduleDTO GetById(string id)
        {
            ScheduleDTO sched = _schedules.FirstOrDefault(s => s.ScheduleId == id);
            if (sched != null)
            {
                return sched;
            }

            return null;
        }

        public List<ShiftDTO> GetShiftsFromSchedule(string id)
        {
            throw new NotImplementedException();
        }

        public List<ShiftDTO> GetTodaysShifts(string id, DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<ScheduleDTO> GetSchedulesFromCompany(string id)
        {
            throw new NotImplementedException();
        }
    }
}
