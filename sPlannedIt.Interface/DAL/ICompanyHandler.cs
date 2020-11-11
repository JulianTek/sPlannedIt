using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Entities.DTOs;

namespace sPlannedIt.Interface.DAL
{
    public interface ICompanyHandler : IHandler<CompanyDTO>
    {
        void AddEmployee(UserDTO user, CompanyDTO company);
        void RemoveEmployee(string id);
        List<string> GetAllEmployees(string id);
        void RemoveAllEmployees(List<string> ids);
    }
}
