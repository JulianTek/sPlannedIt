using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Data;
using sPlannedIt.Interface;
using sPlannedIt.Logic;
using sPlannedIt.Logic.Models;
using sPlannedIt.Viewmodels.Homepage_Viewmodels;
using sPlannedIt.Viewmodels.Schedule_Viewmodels;

namespace sPlannedIt.Controllers
{
    public class EmployerController : Controller
    {
        private readonly ScheduleCollection _schedCollection;
        private readonly ShiftCollection _shiftCollection;
        private readonly CompanyCollection _compCollection;

        public EmployerController()
        {
            _schedCollection = new ScheduleCollection();
            _shiftCollection = new ShiftCollection();
            _compCollection = new CompanyCollection();
        }

        public IActionResult IndexEmployer()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string companyId = _compCollection.GetCompanyFromUser(userId).CompanyID;
            List<Schedule> schedules =
                _schedCollection.GetSchedulesFromCompany(companyId);
            if (schedules.Count > 0)
            {
                IndexEmployerViewModel model = new IndexEmployerViewModel()
                {
                    Schedules = schedules,
                    CompanyID = companyId,
                    TodaysWorkers = _schedCollection.GetTodaysShifts(companyId)
                };
                return View(model);
            }
            else
            {
                IndexEmployerViewModel model = new IndexEmployerViewModel()
                {
                    Schedules = new List<Schedule>(),
                    CompanyID = companyId,
                        TodaysWorkers = _schedCollection.GetTodaysShifts(companyId)
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
                Shift shift = new Shift(model.ShiftId, model.ScheduleId, model.UserId, model.DateTime, model.StartTime,
                    model.EndTime);
                var result = _shiftCollection.Create(shift);
                if (result)
                {
                    return RedirectToAction("EditSchedule", new {id = model.ScheduleId});
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
                Shifts = new List<Shift>()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateSchedule(CreateScheduleViewmodel model)
        {
            if (ModelState.IsValid)
            {
                Schedule schedule = new Schedule(model.ScheduleId, model.CompanyId, model.Name);
                var result = _schedCollection.Create(schedule);
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
            Schedule schedule = _schedCollection.GetSchedule(id);
            EditScheduleViewmodel model = new EditScheduleViewmodel()
            {
                CompanyId = schedule.CompanyID,
                Name = schedule.Name,
                ScheduleId = schedule.ScheduleID,
                Shifts = _schedCollection.GetShiftsFromSchedule(id)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditSchedule(EditScheduleViewmodel model)
        {
            Schedule schedule = new Schedule()
            {
                CompanyID = model.CompanyId,
                Name = model.Name,
                ScheduleID = model.ScheduleId, 
                Shifts = model.Shifts
            };
            _schedCollection.Update(schedule);
            return RedirectToAction("IndexEmployer");
        }
    }
}
