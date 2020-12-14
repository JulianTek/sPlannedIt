using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Data
{
    class MemShiftHandler : IShiftHandler
    {

        private readonly List<ShiftDTO> _shifts = new List<ShiftDTO>();
        public List<ShiftDTO> GetAll()
        {
            return _shifts;
        }

        public bool Create(ShiftDTO entity)
        {
            int oldCount = _shifts.Count;
            _shifts.Add(entity);
            if (oldCount != _shifts.Count)
            {
                return true;
            }

            return false;
        }

        public bool Update(ShiftDTO entity)
        {
            ShiftDTO shift = _shifts.FirstOrDefault(s => s.ShiftId == entity.ShiftId);
            if (shift != null)
            {
                shift.ShiftId = entity.ShiftId;
                shift.ScheduleId = entity.ScheduleId;
                shift.UserId = entity.UserId;
                shift.ShiftDate = entity.ShiftDate;
                shift.StartTime = entity.StartTime;
                shift.EndTime = entity.EndTime;
                return true;
            }

            return false;

        }

        public bool Delete(string id)
        {
            int oldCount = _shifts.Count;
            ShiftDTO s = _shifts.FirstOrDefault(s => s.ShiftId == id);
            _shifts.Remove(s);
            if (oldCount != _shifts.Count)
            {
                return true;
            }

            return false;
        }

        public ShiftDTO GetById(string id)
        {
            ShiftDTO shift = _shifts.FirstOrDefault(s => s.ShiftId == id);
            return shift ?? null;
        }

        public string GetUserFromShift(string id)
        {
            ShiftDTO shift = _shifts.FirstOrDefault(s => s.ShiftId == id);
            if (shift != null)
            {
                return shift.UserId;
            }

            return null;
        }

        public List<ShiftDTO> GetShiftsFromUser(string userId)
        {
            List<ShiftDTO> dtos = new List<ShiftDTO>();
            foreach (ShiftDTO shift in _shifts)
            {
                if (shift.UserId == userId)
                {
                    dtos.Add(shift);
                }
            }

            return dtos;
        }
    }
}
