using System;
using System.Collections.Generic;
using System.Text;

namespace sPlannedIt.Entities.DTOs
{
    public class ScheduleDTO
    {
        public ScheduleDTO()
        {
            
        }

        public ScheduleDTO(string name, string scheduleId, string companyId)
        {
            Name = name;
            ScheduleId = scheduleId;
            CompanyId = companyId;
        }

        public string Name { get; private set; }
        public string ScheduleId { get; private set; }
        public string CompanyId { get; private set; }
    }
}
