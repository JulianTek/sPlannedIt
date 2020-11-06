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
        private readonly ShiftContainer _container = new ShiftContainer();
        private readonly ScheduleContainer _schedContainer = new ScheduleContainer();
        public IActionResult IndexEmployer()
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IndexEmployerViewModel model = new IndexEmployerViewModel()
            {
                CompanyID = Logic.ScheduleManager_Logic.GetCompanyID(userID),
                TodaysWorkers = Logic.ScheduleManager_Logic.ConvertIDsToShifts(Logic.ScheduleManager_Logic.GetTodaysWorkers(Logic.ScheduleManager_Logic.GetScheduleID(DateTime.Today)))
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateShift()
        {
            return View();
        }


        [HttpGet]
        public IActionResult CreateSchedule()
        {
            //Temp way of creating viewmodel, will eventually make use of container
            CreateScheduleViewmodel model = new CreateScheduleViewmodel()
            {
                CompanyId = Logic.ScheduleManager_Logic.GetCompanyID(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                ScheduleId = Guid.NewGuid().ToString(),
                Shifts = new List<Shift>()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateSchedule(CreateScheduleViewmodel model)
        {
            if (ModelState.IsValid)
            {
                Schedule schedule = _schedContainer.CreateSchedule(model.CompanyId);
                var result = Logic.ScheduleManager_Logic.InsertSchedule(schedule);
                if (result)
                {
                    // todo: implement success view
                    return RedirectToAction("IndexEmployer");
                }
                ModelState.AddModelError("", "Cannot create schedule");
            }

            return View();
        }
    }
}
