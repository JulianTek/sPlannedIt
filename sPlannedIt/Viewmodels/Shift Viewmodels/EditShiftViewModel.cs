using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sPlannedIt.Viewmodels.Shift_Viewmodels
{
    public class EditShiftViewModel
    {
        public List<string> EmployeeEmails { get; set; }
        public string ShiftId { get; set; }
        public string ScheduleId { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string UserEmail { get; set; }
        public DateTime DateTime { get; set; }
    }
}
