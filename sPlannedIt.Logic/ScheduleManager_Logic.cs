using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using sPlannedIt.Data;
using sPlannedIt.Data.Models;
using sPlannedIt.Interface;
using sPlannedIt.Logic.Models;

namespace sPlannedIt.Logic
{
    public class ScheduleManager_Logic
    {
        private readonly UserManager<IdentityUser> _userManager;
        private static readonly ScheduleContainer _container = new ScheduleContainer();
        public ScheduleManager_Logic(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public static string GetScheduleID(DateTime dateTime)
        {
            return Data.ScheduleManager_Data.GetScheduleID(dateTime);
        }

        public static List<string> GetTodaysWorkers(string scheduleID)
        {
            return Data.ScheduleManager_Data.GetTodaysWorkers(scheduleID);
        }

        public static string GetCompanyID(string userID)
        {
            return Data.ScheduleManager_Data.GetCompanyID(userID);
        }


        public static List<string> GetScheduleIDs(string companyID)
        {
            return Data.ScheduleManager_Data.GetScheduleIDs(companyID);
        }


        // Converts user IDs in a list to a list of IdentityUsers
        public async Task<List<IdentityUser>> ConvertIDsToUsers(List<string> userIDs)
        {
            List<IdentityUser> users = new List<IdentityUser>();
            foreach (string id in userIDs)
            {
               var user = await _userManager.FindByIdAsync(id);
                users.Add(user);
            }

            return users;
        }

        public static List<string> GetShiftIDs(string userID)
        {
            return Data.ScheduleManager_Data.GetShifts(userID);
        }

        public static List<ShiftDTO> ConvertIdsToDtos(List<string> shiftIDs)
        {
            List<ShiftDTO> shiftDtos = new List<ShiftDTO>();
            foreach (string id in shiftIDs)
            {
                shiftDtos.Add(FindShiftDto(id));
            }

            return shiftDtos;
        }

        public static ShiftDTO FindShiftDto(string shiftID)
        {
            return Data.ScheduleManager_Data.FindShiftDto(shiftID);
        }

        public static bool InsertShift(Shift shift)
        {
            return Data.ScheduleManager_Data.InsertShift(shift.ShiftID, shift.ScheduleID, shift.StartTime,
                shift.EndTime, shift.UserID, shift.ShiftDate);
        }

        public static bool InsertSchedule(Schedule schedule)
        {
            return Data.ScheduleManager_Data.InsertSchedule(schedule.ScheduleID, schedule.CompanyID, schedule.Name);
        }
        public static List<IShift> ConvertIDsToShifts(List<string> ids)
        {
            List<IShift> shifts = new List<IShift>();
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

        public static List<Schedule> ConvertSchedulesList(List<string> ids)
        {
            List<Schedule> schedules = new List<Schedule>();
            foreach (string id in ids)
            {
                Schedule schedule = _container.FindSchedule(id);
                if (schedule == null)
                {
                    ScheduleDTO dto = Data.ScheduleManager_Data.GetSchedule(id);
                    schedule = _container.CreateSchedule(dto.CompanyID, dto.ScheduleID, dto.Name);
                }
                schedules.Add(schedule);
            }

            return schedules;
        }

        public static Schedule ConvertSchedule(string id)
        {
            Schedule schedule = _container.FindSchedule(id);
            if (schedule == null)
            {
                ScheduleDTO dto = Data.ScheduleManager_Data.GetSchedule(id);
                schedule = _container.CreateSchedule(dto.CompanyID, dto.ScheduleID, dto.Name);
                schedule.Shifts = ConvertIDsToShifts(ScheduleManager_Data.GetShiftsFromSched(id));
            }

            return schedule;
        }
    }
}
