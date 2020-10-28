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

        //Todo: Add method that checks if employee already exists in db and program methods that locally add employees if not
        private static readonly CompanyContainer _container = new CompanyContainer();
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

        public static bool CheckIfEmployeeIsInCompany(string userID, string companyID)
        {
            int amount = Data.CompanyManager_Data.CheckIfEmployeeIsInCompany(userID, companyID);

            return amount != 0;
        }

        public static bool AddEmployeeToCompany(string userID, string companyID)
        {
            int amount = Data.CompanyManager_Data.AddEmployeeToCompany(userID, companyID);

            return amount != 0;
        }

        public static bool RemoveEmployeeFromCompany(string userID, string companyID)
        {
            int amount = Data.CompanyManager_Data.RemoveEmployeeFromCompany(userID, companyID);

            return amount != 0;
        }

        public static List<string> GetEmployeesFromCompany(string companyId)
        {
            return Data.CompanyManager_Data.GetEmployees(companyId);
        }

        public static void EditCompany(string id, string name)
        {
            Data.CompanyManager_Data.EditCompany(id, name);
        }

        public static void DeleteCompany(string id)
        {
            RemoveAllEmployees(id);
            Data.CompanyManager_Data.DeleteCompany(id);
        }

        public static void RemoveAllEmployees(string companyId)
        {
            List<string> users = GetEmployeesFromCompany(companyId);

            foreach (string id in users)
            {
                RemoveEmployeeFromCompany(id, companyId);
            }
        }
    }
}
