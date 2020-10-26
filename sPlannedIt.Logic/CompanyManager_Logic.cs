using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Data;
using sPlannedIt.Data.Models;
using sPlannedIt.Logic.Models;

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

        public static Company ConvertDtOtoCompany(CompanyDTO dto)
        {
            if (dto != null)
            {
                return new Company()
                {
                    CompanyID = dto.CompanyID,
                    CompanyName = dto.CompanyName
                };
            }

            return null;
        }

        public static string GetEmployee(string id)
        {
            return Data.CompanyManager_Data.GetEmployee(id);
        }

        public static List<Company> ConvertDtoList(List<CompanyDTO> companyDtos)
        {
            List<Company> companies = new List<Company>();
            foreach (CompanyDTO dto in companyDtos)
            {
                companies.Add(new Company()
                {
                    CompanyID = dto.CompanyID,
                    CompanyName = dto.CompanyName,
                    Employees = dto.Employees
                });
            }

            return companies;
        }
    }
}
