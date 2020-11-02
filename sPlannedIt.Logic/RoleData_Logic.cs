using System;
using System.Collections.Generic;
using System.Text;

namespace sPlannedIt.Logic
{
    public static class RoleData_Logic
    {

        public static List<string> GetRoleNames()
        {
            return Data.Role_functions.RoleData_Data.GetRoleNames();
        }
    }
}
