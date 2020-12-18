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
                    ShiftDTO dto = new ShiftDTO(reader.GetString(0), reader.GetString(1),
                        reader.GetString(5), reader.GetDateTime(4), reader.GetInt32(2), reader.GetInt32(3));
                    dtos.Add(dto);
                }
                connectionString.Dispose();
                return dtos;
            }
        }

        public ShiftDTO Create(ShiftDTO entity)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand create = new SqlCommand("INSERT INTO Shift(ShiftID, ScheduleId, StartTime, EndTime, Date, UserID) VALUES(@ShiftId, @ScheduleId, @StartTime, @EndTime, @Date, @UserID)", connectionString.SqlConnection);
                create.Parameters.AddWithValue("@ShiftId", entity.ShiftId);
                create.Parameters.AddWithValue("@ScheduleId", entity.ScheduleId);
                create.Parameters.AddWithValue("@StartTime", entity.StartTime);
                create.Parameters.AddWithValue("@EndTime", entity.EndTime);
                create.Parameters.AddWithValue("@Date", entity.ShiftDate.Date);
                create.Parameters.AddWithValue("@UserID", entity.UserId);
                connectionString.Open();
                var result = create.ExecuteNonQuery();
                connectionString.Dispose();
                if (result != 0)
                {
                    return entity;
                }

                return null;
            }
        }

        public ShiftDTO Update(ShiftDTO entity)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand update = new SqlCommand("UPDATE Shift SET StartTime = @StartTime, EndTime = @EndTime, Date = @Date, UserID = @UserID", connectionString.SqlConnection);
                update.Parameters.AddWithValue("@StartTime", entity.StartTime);
                update.Parameters.AddWithValue("@EndTime", entity.EndTime);
                update.Parameters.AddWithValue("@Date", entity.ShiftDate.Date);
                update.Parameters.AddWithValue("@UserID", entity.UserId);
                connectionString.Open();
                var result = update.ExecuteNonQuery();
                connectionString.Dispose();
                if (result != 0)
                {
                    return entity;
                }

                return null;
            }
        }

        public bool Delete(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand delete = new SqlCommand("DELETE FROM Shift WHERE ShiftID = @ShiftId", connectionString.SqlConnection);
                delete.Parameters.AddWithValue("@ShiftId", id);
                connectionString.Open();
               var result = delete.ExecuteNonQuery();
                connectionString.Dispose();
                return result != 0;
            }
        }

        public ShiftDTO GetById(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getById = new SqlCommand("SELECT * FROM Shift WHERE ShiftID = @ShiftId", connectionString.SqlConnection);
                getById.Parameters.AddWithValue("@ShiftId", id);
                connectionString.Open();
                var reader = getById.ExecuteReader();
                while (reader.Read())
                {
                    ShiftDTO dto = new ShiftDTO(reader.GetString(0), reader.GetString(1),
                        reader.GetString(5), reader.GetDateTime(4), reader.GetInt32(2), reader.GetInt32(3));
                    connectionString.Dispose();
                    return dto;
                }
                connectionString.Dispose();
                return new ShiftDTO();
            }
        }

        public string GetUserFromShift(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getUser = new SqlCommand("SELECT UserID FROM Shift WHERE ShiftID = @ShiftId", connectionString.SqlConnection);
                getUser.Parameters.AddWithValue("@ShiftId", id);
                var result = (string) getUser.ExecuteScalar();
                return result;
            }
        }

        public string GetUserEmailFromShift(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getEmail = new SqlCommand("SELECT AspNetUsers.Email FROM Shift INNER JOIN AspNetUsers ON Shift.UserEmail = AspNetUsers.Id WHERE ShiftID = @ShiftID", connectionString.SqlConnection);
                getEmail.Parameters.AddWithValue("@ShiftID", id);
                connectionString.Open();
                var result = (string) getEmail.ExecuteScalar();
                connectionString.Dispose();
                return result ?? null;
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
                    ShiftDTO dto = new ShiftDTO(reader.GetString(0), reader.GetString(1),
                        reader.GetString(5), reader.GetDateTime(4), reader.GetInt32(2), reader.GetInt32(3));
                    shiftDtos.Add(dto);
                }
                connectionString.Dispose();
                return shiftDtos;
            }
        }
    }
}
