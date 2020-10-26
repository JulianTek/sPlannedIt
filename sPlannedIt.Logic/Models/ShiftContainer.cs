using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace sPlannedIt.Logic.Models
{
    public class ShiftContainer
    {
        public ShiftContainer()
        {
            AllShifts = new List<Shift>();
        }
        public List<Shift> AllShifts { get; set; }

        public Shift CreateShift(string userId, DateTime shiftDate, int startTime, int endTime)
        {
            Shift shift =  new Shift(userId, shiftDate, startTime, endTime);
            AllShifts.Add(shift);
            return shift;
        }

        public bool RemoveShift(string id)
        {
            foreach (Shift shift in AllShifts)
            {
                if (shift.ShiftID == id)
                {
                    AllShifts.Remove(shift);
                    return true;
                }
            }

            return false;
        }

        public Shift FindShift(string id)
        {
            foreach (Shift shift in AllShifts)
            {
                if (shift.ShiftID == id)
                {
                    return shift;
                }
            }

            return null;
        }


    }
}
