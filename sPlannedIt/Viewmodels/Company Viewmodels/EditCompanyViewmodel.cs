using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sPlannedIt.Viewmodels.Company_Viewmodels
{
    public class EditCompanyViewmodel
    {
        public string CompanyID { get; set; }
        [Required]
        public string CompanyName { get; set; }
    }
}
