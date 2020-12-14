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

        public string Name { get; set; }
        public string ScheduleId { get; set; }
        public string CompanyId { get; set; }
    }
}
