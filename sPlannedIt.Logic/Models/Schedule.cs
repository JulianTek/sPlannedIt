using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sPlannedIt.Interface;

namespace sPlannedIt.Logic.Models
{
    public class Schedule
    {
        public Schedule()
        {

        }
        public Schedule(string companyID, string name)
        {
            Name = name;
            ScheduleID = Guid.NewGuid().ToString();
            CompanyID = companyID;
        }

        public Schedule(string scheduleId, string companyId, string name)
        {
            Name = name;
            ScheduleID = scheduleId;
            CompanyID = companyId;
        }

        public string Name { get; set; }
        public string ScheduleID { get; set; }
        public string CompanyID { get; set; }
        public List<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
