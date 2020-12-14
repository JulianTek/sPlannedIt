using System;
using System.Collections.Generic;
using System.Text;

namespace sPlannedIt.Entities.DTOs
{
    public class CompanyDTO
    {
        public CompanyDTO()
        {

        }


        public CompanyDTO(string companyId, string companyName)
        {
            CompanyId = companyId;
            CompanyName = companyName;
            Employees = new List<string>();
        }

        public CompanyDTO(string companyId, string companyName, List<string> employees)
        {
            CompanyId = companyId;
            CompanyName = companyName;
            Employees = employees;
        }

        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<string> Employees { get; set; }
    }
}
