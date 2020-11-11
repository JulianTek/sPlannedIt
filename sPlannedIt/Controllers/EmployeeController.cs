using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Logic.Models;
using sPlannedIt.Viewmodels.Homepage_Viewmodels;

namespace sPlannedIt.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult IndexEmployee()
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IndexEmployeeViewModel model = new IndexEmployeeViewModel()
            {
                CompanyID = Logic.ScheduleManager_Logic.GetCompanyID(userID),
                Shifts = Logic.ScheduleManager_Logic.ConvertIDsToShifts(Logic.ScheduleManager_Logic.GetShiftIDs(userID)),
                TodaysWorkers = Logic.ScheduleManager_Logic.ConvertIDsToShifts(Logic.ScheduleManager_Logic.GetTodaysWorkers(Logic.ScheduleManager_Logic.GetTodaysScheduleID(DateTime.Today)))
            };
            return View(model);
        }
    }
}