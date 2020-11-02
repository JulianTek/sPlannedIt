using System;
using System.Collections.Generic;
using System.Text;

namespace sPlannedIt.Data.Models
{
    public class CompanyDTO
    {
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
        public List<string> Employees { get; set; }
    }
}
