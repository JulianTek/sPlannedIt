using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Data;
using sPlannedIt.Interface;
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
            List<Schedule> schedules = Logic.ScheduleManager_Logic.ConvertSchedulesList(
                Logic.ScheduleManager_Logic.GetScheduleIDs(Logic.ScheduleManager_Logic.GetCompanyID(userID)));
            if (schedules.Count > 0)
            {
                IndexEmployerViewModel model = new IndexEmployerViewModel()
                {
                    Schedules = schedules,
                    CompanyID = Logic.ScheduleManager_Logic.GetCompanyID(userID),
                    TodaysWorkers = Logic.ScheduleManager_Logic.ConvertIDsToShifts(Logic.ScheduleManager_Logic.GetTodaysWorkers(Logic.ScheduleManager_Logic.GetTodaysScheduleID(DateTime.Today)))
                };
                return View(model);
            }
            else
            {
                IndexEmployerViewModel model = new IndexEmployerViewModel()
                {
                    Schedules = new List<Schedule>(),
                    CompanyID = Logic.ScheduleManager_Logic.GetCompanyID(userID),
                    TodaysWorkers = Logic.ScheduleManager_Logic.ConvertIDsToShifts(Logic.ScheduleManager_Logic.GetTodaysWorkers(Logic.ScheduleManager_Logic.GetTodaysScheduleID(DateTime.Today)))
                };
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult CreateShift(string id)
        {
            CreateShiftViewmodel model = new CreateShiftViewmodel()
            {
                ShiftId = Guid.NewGuid().ToString(),
                ScheduleId = id
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateShift(CreateShiftViewmodel model)
        {
            if (ModelState.IsValid)
            {
                Shift shift = _container.CreateShift(model.UserId, model.DateTime, model.StartTime, model.EndTime,
                    model.ScheduleId, model.ShiftId);
                var result = Logic.ScheduleManager_Logic.InsertShift(shift);
                if (result)
                {
                    return RedirectToAction("EditSchedule", new { id = model.ScheduleId });
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateSchedule(string id)
        {
            CreateScheduleViewmodel model = new CreateScheduleViewmodel()
            {
                CompanyId = id,
                ScheduleId = Guid.NewGuid().ToString(),
                Shifts = new List<IShift>()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateSchedule(CreateScheduleViewmodel model)
        {
            if (ModelState.IsValid)
            {
                Schedule schedule = _schedContainer.CreateSchedule(model.CompanyId, model.ScheduleId, model.Name);
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

        [HttpGet]
        public IActionResult EditSchedule(string id)
        {
            Schedule schedule = Logic.ScheduleManager_Logic.ConvertSchedule(id);
            EditScheduleViewmodel model = new EditScheduleViewmodel()
            {
                CompanyId = schedule.CompanyID,
                Name = schedule.Name,
                ScheduleId = schedule.ScheduleID,
                Shifts = Logic.ScheduleManager_Logic.ConvertIDsToShifts(ScheduleManager_Data.GetShiftsFromSched(id))
        };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditSchedule(EditScheduleViewmodel model)
        {
            Schedule schedule = Logic.ScheduleManager_Logic.ConvertSchedule(model.ScheduleId);
            schedule.UpdateSchedule(model.CompanyId, model.Name);
            return RedirectToAction("IndexEmployer");
        }
    }
}
