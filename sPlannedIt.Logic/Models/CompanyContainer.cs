using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace sPlannedIt.Logic.Models
{
    public class CompanyContainer
    {
        public CompanyContainer()
        {
            AllCompanies = CompanyManager_Logic.ConvertDtoList(CompanyManager_Logic.FindAllCompanies());
        }

        public List<Company> AllCompanies { get; set; }


        public Company CreateCompany(string companyName)
        {
            Company company = new Company(companyName);
            AllCompanies.Add(company);
            return company;
        }

        public bool RemoveCompany(string companyID)
        {
            foreach (Company company in AllCompanies)
            {
                if (company.CompanyID == companyID)
                {
                    AllCompanies.Remove(company);
                    return true;
                }
            }

            return false;
        }

        public Company FindCompany(string id)
        {
            foreach (Company company in AllCompanies)
            {
                if (company.CompanyID == id)
                {
                    return company;
                }
            }

            return null;
        }
    }
}
