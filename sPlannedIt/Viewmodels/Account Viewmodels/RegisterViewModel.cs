using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using sPlannedIt.Models;

namespace sPlannedIt.Viewmodels.Account_Viewmodels
{
    public class RegisterViewModel
    {
        public UserData User { get; set; }
        public RolesData Roles { get; set; }
    }
}
