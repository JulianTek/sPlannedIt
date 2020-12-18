using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Data;

namespace sPlannedIt.Logic
{
    public static class RoleData_Logic
    {

        public static List<string> GetRoleNames()
        {
            return RoleData_Data.GetRoleNames();
        }
    }
}
