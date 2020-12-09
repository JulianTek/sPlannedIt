using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Logic;
using sPlannedIt.Logic.Models;
using sPlannedIt.Viewmodels.Homepage_Viewmodels;

namespace sPlannedIt.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly CompanyCollection _companyCollection;
        private readonly ScheduleCollection _schedCollection;
        private readonly ShiftCollection _shiftCollection;
        public EmployeeController()
        {
            _companyCollection = new CompanyCollection();
            _schedCollection = new ScheduleCollection();
            _shiftCollection = new ShiftCollection();
        }

        public IActionResult IndexEmployee()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IndexEmployeeViewModel model = new IndexEmployeeViewModel()
            {
                CompanyID = _companyCollection.GetCompanyFromUser(userId).CompanyId,
                Shifts = _shiftCollection.GetShiftsFromUser(userId),
                TodaysWorkers = _schedCollection.GetTodaysShifts(_companyCollection.GetCompanyFromUser(userId).CompanyId)
            };
            return View(model);
        }
    }
}