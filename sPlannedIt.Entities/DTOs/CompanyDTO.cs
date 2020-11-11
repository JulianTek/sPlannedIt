using System;
using System.Collections.Generic;
using System.Text;

namespace sPlannedIt.Entities.DTOs
{
    public class CompanyDTO
    {
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
        public List<string> Employees { get; set; }
    }
}
