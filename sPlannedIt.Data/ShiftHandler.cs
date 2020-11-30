using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Data.SqlClient;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Data
{
    public class ShiftHandler : IShiftHandler
    {
        public List<ShiftDTO> GetAll()
        {
            List<ShiftDTO> dtos = new List<ShiftDTO>();
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getAll = new SqlCommand("SELECT * FROM Shift", connectionString.SqlConnection);
                connectionString.Open();
                var reader = getAll.ExecuteReader();
                while (reader.Read())
                {
                    ShiftDTO dto = new ShiftDTO()
                    {
                        ShiftID = reader.GetString(0),
                        ScheduleID = reader.GetString(1),
                        StartTime = reader.GetInt32(2),
                        EndTime = reader.GetInt32(3),
                        ShiftDate = reader.GetDateTime(4),
                        UserID = reader.GetString(5)
                    };
                    dtos.Add(dto);
                }
                connectionString.Dispose();
                return dtos;
            }
        }

        public bool Create(ShiftDTO entity)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand create = new SqlCommand("INSERT INTO Shift(ShiftID, ScheduleID, StartTime, EndTime, Date, UserID) VALUES(@ShiftID, @ScheduleID, @StartTime, @EndTime, @Date, @UserID)", connectionString.SqlConnection);
                create.Parameters.AddWithValue("@ShiftID", entity.ShiftID);
                create.Parameters.AddWithValue("@ScheduleID", entity.ScheduleID);
                create.Parameters.AddWithValue("@StartTime", entity.StartTime);
                create.Parameters.AddWithValue("@EndTime", entity.EndTime);
                create.Parameters.AddWithValue("@Date", entity.ShiftDate.Date);
                create.Parameters.AddWithValue("@UserID", entity.UserID);
                connectionString.Open();
                var result = create.ExecuteNonQuery();
                connectionString.Dispose();
                return result != 0;
            }
        }

        public bool Update(ShiftDTO entity)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand update = new SqlCommand("UPDATE Shift SET StartTime = @StartTime, EndTime = @EndTime, Date = @Date, UserID = @UserID", connectionString.SqlConnection);
                update.Parameters.AddWithValue("@StartTime", entity.StartTime);
                update.Parameters.AddWithValue("@EndTime", entity.EndTime);
                update.Parameters.AddWithValue("@Date", entity.ShiftDate.Date);
                update.Parameters.AddWithValue("@UserID", entity.UserID);
                connectionString.Open();
                var result = update.ExecuteNonQuery();
                connectionString.Dispose();
                return result != 0;
            }
        }

        public bool Delete(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand delete = new SqlCommand("DELETE FROM Shift WHERE ShiftID = @ShiftID", connectionString.SqlConnection);
                delete.Parameters.AddWithValue("@ShiftID", id);
                connectionString.Open();
               var result = delete.ExecuteNonQuery();
                connectionString.Dispose();
                return result != 0;
            }
        }

        public ShiftDTO GetById(string id)
        {
            ShiftDTO dto = new ShiftDTO();
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getById = new SqlCommand("SELECT * FROM Shift WHERE ShiftID = @ShiftID", connectionString.SqlConnection);
                getById.Parameters.AddWithValue("@ShiftID", id);
                connectionString.Open();
                var reader = getById.ExecuteReader();
                while (reader.Read())
                {
                    dto.ShiftID = reader.GetString(0);
                    dto.ScheduleID = reader.GetString(1);
                    dto.StartTime = reader.GetInt32(2);
                    dto.EndTime = reader.GetInt32(3);
                    dto.ShiftDate = reader.GetDateTime(4);
                    dto.UserID = reader.GetString(5);
                }
                connectionString.Dispose();
                return dto;
            }
        }

        public string GetUserFromShift(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getUser = new SqlCommand("SELECT UserID FROM Shift WHERE ShiftID = @ShiftID", connectionString.SqlConnection);
                getUser.Parameters.AddWithValue("@ShiftID", id);
                var result = (string) getUser.ExecuteScalar();
                return result;
            }
        }

        public List<ShiftDTO> GetShiftsFromUser(string userId)
        {
            List<ShiftDTO> shiftDtos = new List<ShiftDTO>();
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getShift = new SqlCommand("SELECT * FROM Shift WHERE UserID = @UserID", connectionString.SqlConnection);
                getShift.Parameters.AddWithValue("@UserID", userId);
                connectionString.Open();
                var reader = getShift.ExecuteReader();
                while (reader.Read())
                {
                    ShiftDTO dto = new ShiftDTO()
                    {
                        ShiftID = reader.GetString(0),
                        ScheduleID = reader.GetString(1),
                        StartTime = reader.GetInt32(2),
                        EndTime = reader.GetInt32(3),
                        ShiftDate = reader.GetDateTime(4),
                        UserID = reader.GetString(5)
                    };
                    shiftDtos.Add(dto);
                }
                connectionString.Dispose();
                return shiftDtos;
            }
        }
    }
}
