using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Models;
using sPlannedIt.Viewmodels.Homepage_Viewmodels;

namespace sPlannedIt.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        public IActionResult IndexEmployee()
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IndexEmployeeViewModel model = new IndexEmployeeViewModel()
            {
                CompanyID = Logic.ScheduleManager_Logic.GetCompanyID(userID),
                Shifts = ConvertIDsToShifts(Logic.ScheduleManager_Logic.GetShiftIDs(userID)),
                TodaysWorkers = ConvertIDsToShifts(Logic.ScheduleManager_Logic.GetTodaysWorkers(Logic.ScheduleManager_Logic.GetScheduleID(DateTime.Today)))
            };
            return View(model);
        }

        // Following methods are temp while I figure out how to put these in the logic layer
        public List<Shift> ConvertIDsToShifts(List<string> ids)
        {
            List<Shift> shifts = new List<Shift>();
            foreach (Shift shift in shifts)
            {
                shifts.Add(shift);
            }

            return shifts;
        }
    }
}
