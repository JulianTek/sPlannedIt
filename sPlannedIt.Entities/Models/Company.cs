using System;
using System.Collections.Generic;

namespace sPlannedIt.Entities.Models
{
    public class Company
    {
        public Company()
        {
            Employees = new List<string>();
        }


        public Company(string id, string name)
        {
            CompanyId = id;
            CompanyName = name;
            Employees = new List<string>();
        }

        public Company(string id, string name, List<string> employees)
        {
            CompanyId = id;
            CompanyName = name;
            Employees = employees;
        }

        public Company(string companyName) 
        {
            CompanyId = Guid.NewGuid().ToString();
            CompanyName = companyName;
            Employees = new List<string>();
        }

        public string CompanyId { get; private set; }
        public string CompanyName { get; private set; }
        public List<string> Employees{ get; private set; }


        public void SetEmployees(List<string> emlpoyees)
        {
            Employees = emlpoyees;
        }
    }
}
