using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Data;
using sPlannedIt.Interface;
using sPlannedIt.Logic;
using sPlannedIt.Logic.Models;
using sPlannedIt.Viewmodels.Homepage_Viewmodels;
using sPlannedIt.Viewmodels.Schedule_Viewmodels;
using sPlannedIt.Viewmodels.Shift_Viewmodels;

namespace sPlannedIt.Controllers
{
    public class EmployerController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ScheduleCollection _schedCollection;
        private readonly ShiftCollection _shiftCollection;
        private readonly CompanyCollection _compCollection;

        public EmployerController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _schedCollection = new ScheduleCollection();
            _shiftCollection = new ShiftCollection();
            _compCollection = new CompanyCollection();
        }

        public IActionResult IndexEmployer()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string companyId = _compCollection.GetCompanyFromUser(userId).CompanyId;
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
        public async Task<IActionResult> CreateShift(string id)
        {
            CreateShiftViewmodel model = new CreateShiftViewmodel()
            {
                EmployeeEmails = await GetUserEmails(),
                ShiftId = Guid.NewGuid().ToString(),
                ScheduleId = id,
                DateTime = DateTime.Today
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShift(CreateShiftViewmodel model)
        {
            if (ModelState.IsValid)
            {
                if (!(model.EndTime <= model.StartTime))
                {
                    if (model.StartTime > 23 || model.EndTime > 24)
                    {
                        ModelState.AddModelError("", "Cannot schedule a shift past 11 PM");
                    }
                    else
                    {
                        var user = await _userManager.FindByEmailAsync(model.UserEmail);
                        Shift shift = new Shift(model.ShiftId, model.ScheduleId, user.Id, model.DateTime, model.StartTime,
                            model.EndTime);
                        var result = _shiftCollection.Create(shift);
                        if (result)
                        {
                            return RedirectToAction("EditSchedule", new { id = model.ScheduleId });
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "End time cannot be smaller than start time");
                }
            }



            model.EmployeeEmails = await GetUserEmails();
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
                CompanyId = schedule.CompanyId,
                Name = schedule.Name,
                ScheduleId = schedule.ScheduleId,
                Shifts = _schedCollection.GetShiftsFromSchedule(id)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditSchedule(EditScheduleViewmodel model)
        {
            Schedule schedule = new Schedule(model.ScheduleId, model.CompanyId, model.Name);
            _schedCollection.Update(schedule);
            return RedirectToAction("IndexEmployer");
        }

        [HttpGet]
        public async Task<IActionResult> EditShift(string id)
        {
            Shift shift = _shiftCollection.GetById(id);
            EditShiftViewModel model = new EditShiftViewModel()
            {
                EmployeeEmails = await GetUserEmails(),
                ShiftId = shift.ShiftId,
                ScheduleId = shift.ScheduleId,
                DateTime = shift.ShiftDate,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditShift(EditShiftViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.UserEmail);
            Shift shift = new Shift(model.ShiftId, model.ScheduleId, user.Id, model.DateTime, model.StartTime, model.EndTime);
            var result = _shiftCollection.Update(shift);
            if (result)
            {
                return RedirectToAction("EditSchedule", new {id = model.ScheduleId});
            }

            model.EmployeeEmails = await GetUserEmails();
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteShift(string id)
        {
            var shift = _shiftCollection.GetById(id);
            var scheduleId = shift.ScheduleId;
            var result = _shiftCollection.Delete(id);
            if (result)
            {
                return RedirectToAction("EditSchedule", new {id = scheduleId});
            }

            return RedirectToAction("IndexEmployer");
        }

        [HttpPost]
        public IActionResult DeleteSchedule(string id)
        {
            List <Shift> shifts = _schedCollection.GetShiftsFromSchedule(id);
            foreach (Shift shift in shifts)
            {
                _shiftCollection.Delete(shift.ShiftId);
            }
            _schedCollection.Delete(id);
            return RedirectToAction("IndexEmployer");
        }

        private async Task<List<string>> GetUserEmails()
        {
            string companyId = _compCollection.GetCompanyFromUser(User.FindFirstValue(ClaimTypes.NameIdentifier)).CompanyId;
            List<string> userIds = _compCollection.GetAllEmployees(_compCollection.GetCompany(companyId));
            List<string> userEmails = new List<string>();
            foreach (string userId in userIds)
            {
                var user = await _userManager.FindByIdAsync(userId);
                userEmails.Add(user.Email);
            }

            return userEmails;
        }
    }
}
