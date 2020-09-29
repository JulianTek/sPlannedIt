using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace sPlannedIt.Logic
{
    public class ScheduleManager_Logic
    {
        private readonly UserManager<IdentityUser> _userManager;

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
    }
}
