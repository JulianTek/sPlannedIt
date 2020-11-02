using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace sPlannedIt.Models
{
    public class CompanyDetailEmployeeData
    {
        public IdentityUser User { get; set; }
        public IdentityRole Role { get; set; }
    }
}
