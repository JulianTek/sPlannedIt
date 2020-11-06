using System.Collections.Generic;

namespace sPlannedIt.Interface
{
    public interface ICompany
    {
        string CompanyID { get; set; }
        string CompanyName { get; set; }
        List<string> Employees { get; set; }
        ICompany UpdateCompanyName(string name);
        bool AddEmployee(string id);
        bool RemoveEmployee(string id);
    }
}