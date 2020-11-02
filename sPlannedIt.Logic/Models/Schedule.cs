using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sPlannedIt.Logic.Models
{
    public class Schedule
    {
        public Schedule(string companyID)
        {
            ScheduleID = Guid.NewGuid().ToString();
            CompanyID = companyID;
            Shifts = new List<Shift>();
        }

        public string ScheduleID { get; set; }
        public string CompanyID { get; set; }
        public List<Shift> Shifts { get; set; }

        public bool AddShift(Shift shift)
        {
            int shiftIndex = Shifts.Count;
            Shifts.Add(shift);
            if (Shifts.Count != shiftIndex)
            {
                return true;
            }

            return false;
        }

        public bool RemoveShift(Shift shift)
        {
            if (Shifts.Contains(shift))
            {
                Shifts.Remove(shift);
                return true;
            }

            return false;
        }

        public Schedule UpdateSchedule(string companyID)
        {
            CompanyID = companyID;
            return this;
        }
    }
}
