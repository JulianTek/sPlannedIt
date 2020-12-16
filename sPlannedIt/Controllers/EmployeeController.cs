using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using sPlannedIt.Interface.DAL;
using sPlannedIt.Logic;
using sPlannedIt.Viewmodels.Homepage_Viewmodels;

namespace sPlannedIt.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IShiftHandler _shiftHandler;
        private readonly IScheduleHandler _scheduleHandler;
        private readonly ICompanyHandler _companyHandler;
        public EmployeeController(IShiftHandler shiftHandler, IScheduleHandler scheduleHandler, ICompanyHandler companyHandler)
        {
            _shiftHandler = shiftHandler;
            _scheduleHandler = scheduleHandler;
            _companyHandler = companyHandler;
        }

        public IActionResult IndexEmployee()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IndexEmployeeViewModel model = new IndexEmployeeViewModel()
            {
                CompanyID = _companyHandler.GetCompanyFromUser(userId).CompanyId,
                Shifts = ModelConverter.ConvertShiftDtoListToShiftModelList(_shiftHandler.GetShiftsFromUser(userId)),
                TodaysWorkers = ModelConverter.ConvertShiftDtoListToShiftModelList(_scheduleHandler.GetTodaysShifts(_companyHandler.GetCompanyFromUser(userId).CompanyId, DateTime.Today))
            };
            return View(model);
        }
    }
}