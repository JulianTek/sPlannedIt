using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sPlannedIt.Entities.Models;
using sPlannedIt.Models;

namespace sPlannedIt.Viewmodels.Company_Viewmodels
{
    public class CompanyDetailsViewmodel
    {
        public Company Company { get; set; }
        public List<CompanyDetailEmployeeData> EmployeeData { get; set; }
    }
}
