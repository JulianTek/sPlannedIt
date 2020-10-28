using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace sPlannedIt.Logic.Models
{
    public class Company
    {
        public Company()
        {
            Employees = new List<string>();
        }


        public Company(string id, string name)
        {
            CompanyID = id;
            CompanyName = name;
            Employees = new List<string>();
        }

        public Company(string companyName) 
        {
            CompanyID = Guid.NewGuid().ToString();
            CompanyName = companyName;
            Employees = new List<string>();
        }

        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
        public List<string> Employees{ get; set; }

        public Company UpdateCompanyName(string name)
        {
            CompanyName = name;
            return this;
        }

        public bool AddEmployee(string id)
        {
            int count = Employees.Count;
            Employees.Add(id);
            if (count != Employees.Count)
            {
                return true;
            }

            return false;
        }

        public bool RemoveEmployee(string id)
        {
            if (Employees.Contains(id))
            {
                Employees.Remove(id);
                return true;
            }

            return false;
        }
    }
}
