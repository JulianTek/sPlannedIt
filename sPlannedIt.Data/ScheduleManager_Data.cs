﻿using System;
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
                    connectionString.sqlConnection);
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


        // Gets the company from the logged in user
        public static string GetCompanyID(string userID)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getCompanyID = new SqlCommand("SELECT CompanyID from UserCompanyLink WHERE @userID = userID",
                    connectionString.sqlConnection);
                getCompanyID.Parameters.AddWithValue("@userID", userID);
                connectionString.sqlConnection.Open();
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
                SqlCommand getTodaysWorkers = new SqlCommand("SELECT userID from ShiftUserLink WHERE @ScheduleID = ScheduleID", connectionString.sqlConnection);
                getTodaysWorkers.Parameters.AddWithValue("@ScheduleID", scheduleID);
                connectionString.sqlConnection.Open();
                var reader = getTodaysWorkers.ExecuteReader();
                while (reader.Read())
                {
                    todaysWorkers.Add(reader.GetString(0));
                }

                connectionString.Dispose();
                return todaysWorkers;
            }
        }


        // Gets the schedule ID from today's date
        //Todo: Figure out how to add parameter for company
        public static string GetScheduleID(DateTime dateTime)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getShiftID = new SqlCommand("SELECT ScheduleID from Shift WHERE @Date = Date", connectionString.sqlConnection);
                getShiftID.Parameters.AddWithValue("@Date", dateTime.Date);
                connectionString.sqlConnection.Open();
                string result = getShiftID.ExecuteScalar().ToString();
                connectionString.Dispose();
                return result;
            }
        }


        // Gets all userIDs from a specific company
        public static List<string> GetUserIDs(string companyID)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                List<string> userIDs = new List<string>();
                SqlCommand getUserIDs = new SqlCommand("SELECT userID from UserCompanyLink WHERE @CompanyID = CompanyID", connectionString.sqlConnection);
                getUserIDs.Parameters.AddWithValue("@CompanyID", companyID);
                connectionString.sqlConnection.Open();
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
                SqlCommand getShifts = new SqlCommand("SELECT ShiftID from Shift WHERE @UserID = UserID");
                getShifts.Parameters.AddWithValue("@UserID", userID);
                connectionString.sqlConnection.Open();
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
                SqlCommand findShiftDto =  new SqlCommand("SELECT * FROM Shift WHERE @ShiftID = ShiftID");
                findShiftDto.Parameters.AddWithValue("@ShiftID", shiftID);
                connectionString.sqlConnection.Open();
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
    }
}
