using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Interface.BLL;
using sPlannedIt.Interface.DAL;
using sPlannedIt.Logic;
using sPlannedIt.Viewmodels.Homepage_Viewmodels;
using sPlannedIt.Viewmodels.Schedule_Viewmodels;

namespace sPlannedIt.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IShiftCollection _shiftCollection;
        private readonly IScheduleCollection _scheduleCollection;
        private readonly ICompanyHandler _companyHandler;
        public EmployeeController(IShiftCollection shiftCollection, IScheduleCollection scheduleCollection, ICompanyHandler companyHandler)
        {
            _shiftCollection = shiftCollection;
            _scheduleCollection = scheduleCollection;
            _companyHandler = companyHandler;
        }

        public IActionResult IndexEmployee()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IndexEmployeeViewModel model = new IndexEmployeeViewModel()
            {
                CompanyID = _companyHandler.GetCompanyFromUser(userId).CompanyId,
                Schedules = _scheduleCollection.GetSchedulesFromCompany(_companyHandler.GetCompanyFromUser(userId).CompanyId),
                Shifts = _shiftCollection.GetShiftsFromUser(userId),
                TodaysWorkers = _scheduleCollection.GetTodaysShifts(_companyHandler.GetCompanyFromUser(userId).CompanyId, DateTime.Today)
            };
            return View(model);
        }

        public IActionResult ViewScheduleDetails(string id)
        {
            var sched = _scheduleCollection.GetById(id);
            ScheduleDetailsViewModel model = new ScheduleDetailsViewModel()
            {
                Name = sched.Name,
                Shifts = sched.Shifts
            };
            return View(model);
        }
    }
}