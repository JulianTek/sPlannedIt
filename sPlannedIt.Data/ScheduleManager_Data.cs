using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace sPlannedIt.Data
{
    public static class ScheduleManager_Data
    {
        public static List<string> GetDayIDs(string companyID)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                List<string> scheduleIDs = new List<string>();
                SqlCommand getSchedules = new SqlCommand("SELECT ScheduleID from Schedule WHERE @companyID = companyID", connectionString.sqlConnection);
                getSchedules.Parameters.AddWithValue("@companyID", companyID);
                connectionString.sqlConnection.Open();
                var reader = getSchedules.ExecuteReader();
                while (reader.Read())
                {
                    scheduleIDs.Add(reader.GetString(0));
                }
                connectionString.Dispose();
                return scheduleIDs;
            }
        }

        public static string GetCompanyID(string userID)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getCompanyID = new SqlCommand("SELECT CompanyID from UserCompanyLink WHERE @userID = userID", connectionString.sqlConnection);
                getCompanyID.Parameters.AddWithValue("@userID", userID);
                connectionString.sqlConnection.Open();
                return getCompanyID.ExecuteScalar().ToString();
            }

        }
}
