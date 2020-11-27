using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using sPlannedIt.Interface;

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

    }
}
