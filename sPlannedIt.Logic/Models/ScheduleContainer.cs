using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sPlannedIt.Logic.Models
{
    public class ScheduleContainer
    {
        public List<Schedule> AllSchedules { get; set; } = new List<Schedule>();


        public Schedule CreateSchedule(string companyId, string scheduleId, string name)
        {
            Schedule schedule = new Schedule(scheduleId, companyId, name);
            AllSchedules.Add(schedule);
            return schedule;
        }

        public bool DeleteSchedule(string id)
        {
            Schedule schedule = FindSchedule(id);
            if (AllSchedules.Contains(schedule))
            {
                AllSchedules.Remove(schedule);
                return true;
            }

            return false;
        } 

        public Schedule FindSchedule(string id)
        {
            foreach (Schedule schedule in AllSchedules)
            {
                if (schedule.ScheduleID == id)
                {
                    return schedule;
                }
            }

            return null;
        }
    }
}
