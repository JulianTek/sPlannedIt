using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sPlannedIt.Models;

namespace sPlannedIt.Viewmodels.Account_Viewmodels
{
    public class RegisterEmployeeViewModel
    {
        public UserData User { get; set; }
        public string CompanyId { get; set; }

        public List<string> RoleNames { get; set; } = new List<string> {"Employer", "Employee"};
    }
}
