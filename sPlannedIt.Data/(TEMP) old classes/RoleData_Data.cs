using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace sPlannedIt.Data
{
    public static class RoleData_Data
    {

        public static List<string> GetRoleNames()
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                List<string> roleNames = new List<string>();
                SqlCommand getRoles = new SqlCommand("SELECT Name from AspNetRoles", connectionString.SqlConnection);
                connectionString.Open();
                var reader = getRoles.ExecuteReader();
                while (reader.Read())
                {
                    roleNames.Add(reader.GetString(0));
                }
                connectionString.Dispose();
                return roleNames;
            }
        }
    }
}
