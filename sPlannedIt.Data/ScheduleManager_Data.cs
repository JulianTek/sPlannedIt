using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using sPlannedIt.Data.Models;

namespace sPlannedIt.Data
{
    public class ScheduleManager_Data
    {

        // Gets all saved schedules from a specific company
        public static List<string> GetScheduleIDs(string companyID)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                List<string> scheduleIDs = new List<string>();
                SqlCommand getSchedules = new SqlCommand("SELECT ScheduleID from Schedule WHERE @companyID = companyID",
                    connectionString.SqlConnection);
                getSchedules.Parameters.AddWithValue("@companyID", companyID);
                connectionString.SqlConnection.Open();
                var reader = getSchedules.ExecuteReader();
                while (reader.Read())
                {
                    scheduleIDs.Add(reader.GetString(0));
                }

                connectionString.Dispose();
                return scheduleIDs;
            }
        }


        // Gets the company from the logged in user
        public static string GetCompanyID(string userID)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getCompanyID = new SqlCommand("SELECT CompanyID from UserCompanyLink WHERE @userID = userID",
                    connectionString.SqlConnection);
                getCompanyID.Parameters.AddWithValue("@userID", userID);
                connectionString.SqlConnection.Open();
                string result = getCompanyID.ExecuteScalar().ToString();
                connectionString.Dispose();
                return result;
            }

        }

        // Gets all the workers who work during a specific day
        public static List<string> GetTodaysWorkers(string scheduleID)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                List<string> todaysWorkers = new List<string>();
                SqlCommand getTodaysWorkers =
                    new SqlCommand("SELECT userID from ShiftUserLink WHERE @ScheduleID = ScheduleID",
                        connectionString.SqlConnection);
                getTodaysWorkers.Parameters.AddWithValue("@ScheduleID", scheduleID);
                connectionString.SqlConnection.Open();
                try
                {
                    var reader = getTodaysWorkers.ExecuteReader();
                    while (reader.Read())
                    {
                        todaysWorkers.Add(reader.GetString(0));
                    }

                    connectionString.Dispose();
                    return todaysWorkers;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                    return new List<string>();
                }


            }
        }


        // Gets the schedule ID from today's date
        //Todo: Figure out how to add parameter for company
        public static string GetScheduleID(DateTime dateTime)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getShiftID = new SqlCommand("SELECT ScheduleID from Shift WHERE @Date = Date",
                    connectionString.SqlConnection);
                getShiftID.Parameters.AddWithValue("@Date", dateTime.Date);
                connectionString.SqlConnection.Open();
                try
                {
                    string result = (string)getShiftID.ExecuteScalar();
                    connectionString.Dispose();
                    return result;
                }
                catch (Exception ex)
                {
                    return "null";
                }
            }
        }


        // Gets all userIDs from a specific company
        public static List<string> GetUserIDs(string companyID)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                List<string> userIDs = new List<string>();
                SqlCommand getUserIDs =
                    new SqlCommand("SELECT userID from UserCompanyLink WHERE @CompanyID = CompanyID",
                        connectionString.SqlConnection);
                getUserIDs.Parameters.AddWithValue("@CompanyID", companyID);
                connectionString.SqlConnection.Open();
                var reader = getUserIDs.ExecuteReader();
                while (reader.Read())
                {
                    userIDs.Add(reader.GetString(0));
                }

                connectionString.Dispose();
                return userIDs;
            }
        }

        public static List<string> GetShifts(string userID)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                List<string> shiftIds = new List<string>();
                SqlCommand getShifts = new SqlCommand("SELECT ShiftID from Shift WHERE @UserID = UserID",
                    connectionString.SqlConnection);
                getShifts.Parameters.AddWithValue("@UserID", userID);
                connectionString.SqlConnection.Open();
                var reader = getShifts.ExecuteReader();
                while (reader.Read())
                {
                    shiftIds.Add(reader.GetString(0));
                }

                connectionString.Dispose();
                return shiftIds;
            }
        }

        public static ShiftDTO FindShiftDto(string shiftID)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand findShiftDto = new SqlCommand("SELECT * FROM Shift WHERE @ShiftID = ShiftID");
                findShiftDto.Parameters.AddWithValue("@ShiftID", shiftID);
                connectionString.SqlConnection.Open();
                var reader = findShiftDto.ExecuteReader();
                ShiftDTO shiftDto = new ShiftDTO();
                while (reader.Read())
                {
                    shiftDto.ShiftID = reader.GetString(0);
                    shiftDto.ScheduleID = reader.GetString(1);
                    shiftDto.StartTime = reader.GetInt16(2);
                    shiftDto.EndTime = reader.GetInt16(3);
                    shiftDto.ShiftDate = reader.GetDateTime(4);
                    shiftDto.UserID = reader.GetString(5);
                }

                return shiftDto;
            }
        }

        public static bool InsertShift(string shiftId, string scheduleId, int startTime, int endTime, string userId,
            DateTime date)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand insert = new SqlCommand("INSERT INTO Shift(ShiftID, ScheduleID, StartTime, EndTime, Date, UserID) VALUES(@ShiftID, @ScheduleID, @StartTime, @EndTime, @Date, @UserID)", connectionString.SqlConnection);
                insert.Parameters.AddWithValue("@ShiftID", shiftId);
                insert.Parameters.AddWithValue("@ScheduleID", scheduleId);
                insert.Parameters.AddWithValue("StartTime", startTime);
                insert.Parameters.AddWithValue("@EndTime", endTime);
                insert.Parameters.AddWithValue("@Date", date.Date);
                insert.Parameters.AddWithValue("@UserID", userId);
                connectionString.SqlConnection.Open();
               var result = insert.ExecuteNonQuery();
                connectionString.Dispose();
               return result != 0;

            }
        }

        public static bool InsertSchedule(string scheduleId, string companyId)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand insert = new SqlCommand("INSERT INTO Schedule(ScheduleID, CompanyID) VALUES(@ScheduleID, @CompanyID)", connectionString.SqlConnection);
                insert.Parameters.AddWithValue("@ScheduleID", scheduleId);
                insert.Parameters.AddWithValue("@CompanyID", companyId);
                connectionString.SqlConnection.Open();
                var result = insert.ExecuteNonQuery();
                connectionString.Dispose();
                return result != 0;
            }
        }
    }
}
