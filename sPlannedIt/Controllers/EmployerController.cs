using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Data.Models;
using sPlannedIt.Logic.Models;
using sPlannedIt.Viewmodels.Homepage_Viewmodels;
using sPlannedIt.Viewmodels.Schedule_Viewmodels;

namespace sPlannedIt.Controllers
{
    public class EmployerController : Controller
    {
        private ShiftContainer _container = new ShiftContainer();
        public IActionResult IndexEmployer()
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IndexEmployerViewModel model = new IndexEmployerViewModel()
            {

                CompanyID = Logic.ScheduleManager_Logic.GetCompanyID(userID),
                TodaysWorkers = ConvertIDsToShifts(Logic.ScheduleManager_Logic.GetTodaysWorkers(Logic.ScheduleManager_Logic.GetScheduleID(DateTime.Today)))
            };
            return View(model);
        }


        // Like before, this is temp code
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
