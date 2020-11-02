using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Data.Models;
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
                Shifts = ConvertIDsToShifts(Logic.ScheduleManager_Logic.GetShiftIDs(userID)),
                TodaysWorkers = ConvertIDsToShifts(Logic.ScheduleManager_Logic.GetTodaysWorkers(Logic.ScheduleManager_Logic.GetScheduleID(DateTime.Today)))
            };
            return View(model);
        }

        // Following methods are temp while I figure out how to put these in the logic layer
        public List<Shift> ConvertIDsToShifts(List<string> ids)
        {
            List<Shift> shifts = new List<Shift>();
            List<ShiftDTO> shiftDtos = Logic.ScheduleManager_Logic.ConvertIdsToDtos(ids);
            foreach (ShiftDTO shiftDto in shiftDtos)
            {
                Shift shift = new Shift()
                {
                    ShiftID = shiftDto.ShiftID,
                    ScheduleID = shiftDto.ScheduleID,
                    ShiftDate = shiftDto.ShiftDate,
                    UserID = shiftDto.UserID,
                    StartTime = shiftDto.StartTime,
                    EndTime = shiftDto.EndTime
                };
                shifts.Add(shift);
            }

            return shifts;
        }
    }
}