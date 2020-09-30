using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Data;
using sPlannedIt.Data.Models;

namespace sPlannedIt.Logic
{
    public class CompanyManager_Logic
    {
        public static CompanyDTO AddCompanyDto(string id, string name)
        {
            var succeeded = CompanyManager_Data.CreateCompany(id, name);
            if (succeeded)
            {
                return new CompanyDTO()
                {
                    CompanyID = id,
                    CompanyName = name
                };
            }
            // null for now, might make a custom exception for this
            return null;
        }

        public static List<CompanyDTO> FindAllCompanies()
        {
            return Data.CompanyManager_Data.FindAllCompanies();
        }
    }
}
