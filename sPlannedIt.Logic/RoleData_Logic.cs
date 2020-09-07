using System;
using System.Collections.Generic;
using System.Text;

namespace sPlannedIt.Logic
{
    class RoleData_Logic
    {

        public static List<string> GetRoleNames()
        {
            return Data.Role_functions.RoleData_Data.GetRoleNames();
        }
    }
}
