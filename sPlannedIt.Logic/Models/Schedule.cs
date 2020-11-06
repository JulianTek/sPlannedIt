using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sPlannedIt.Interface;

namespace sPlannedIt.Logic.Models
{
    public class Schedule : ISchedule
    {
        public Schedule(string companyID)
        {
            ScheduleID = Guid.NewGuid().ToString();
            CompanyID = companyID;
        }

        public Schedule(string scheduleId, string companyId)
        {
            ScheduleID = scheduleId;
            CompanyID = companyId;
        }

        public string ScheduleID { get; set; }
        public string CompanyID { get; set; }
        public List<IShift> Shifts { get; set; } = new List<IShift>();

        public bool AddShift(IShift shift)
        {
            int shiftIndex = Shifts.Count;
            Shifts.Add(shift);
            if (Shifts.Count != shiftIndex)
            {
                return true;
            }

            return false;
        }

        public bool RemoveShift(IShift shift)
        {
            if (Shifts.Contains(shift))
            {
                Shifts.Remove(shift);
                return true;
            }

            return false;
        }

        public ISchedule UpdateSchedule(string companyID)
        {
            CompanyID = companyID;
            return this;
        }
    }
}
