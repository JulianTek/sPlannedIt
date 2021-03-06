﻿using System;
using System.Collections.Generic;

namespace sPlannedIt.Entities.Models
{
    public class Schedule
    {
        public Schedule()
        {

        }
        public Schedule(string companyID, string name)
        {
            Name = name;
            ScheduleId = Guid.NewGuid().ToString();
            CompanyId = companyID;
        }

        public Schedule(string scheduleId, string companyId, string name)
        {
            Name = name;
            ScheduleId = scheduleId;
            CompanyId = companyId;
        }

        public Schedule(string scheduleId, string companyId, string name, List<Shift> shifts)
        {
            ScheduleId = scheduleId;
            CompanyId = companyId;
            Name = name;
            Shifts = shifts;
        }

        public string Name { get; private set; }
        public string ScheduleId { get; private set; }
        public string CompanyId { get; private set; }
        public List<Shift> Shifts { get; set; } = new List<Shift>();
    }
}
