using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Entities.DTOs;

namespace sPlannedIt.Interface.DAL
{
    public interface ICompanyHandler : IHandler<CompanyDTO>
    {
        CompanyDTO GetCompanyFromUser(string userId);
        bool AddEmployee(string userId, CompanyDTO company);
        bool RemoveEmployee(string id);
        List<string> GetAllEmployees(string id);
        List<string> GetAllEmployeeEmails(string id);
        void RemoveAllEmployees(List<string> ids);
        bool CheckIfCompanyNameExists(string name);
        bool CheckIfEmployeeInCompany(string userId, string companyId);
    }
}
