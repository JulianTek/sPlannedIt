using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Data
{
    class ScheduleHandler : IScheduleHandler
    {
        public List<ScheduleDTO> GetAll()
        {
            List<ScheduleDTO> dtos = new List<ScheduleDTO>();
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getAll = new SqlCommand("SELECT * FROM Schedule", connectionString.SqlConnection);
                connectionString.Open();
                var reader = getAll.ExecuteReader();
                while (reader.Read())
                {
                    ScheduleDTO dto = new ScheduleDTO()
                    {
                        ScheduleID = reader.GetString(1),
                        CompanyID = reader.GetString(2),
                        Name = reader.GetString(3)
                    };
                    dtos.Add(dto);
                }
                connectionString.Dispose();
                return dtos;
            }
        }

        public void Create(ScheduleDTO entity)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand create = new SqlCommand("INSERT INTO Schedule(ScheduleID, CompanyID, Name) VALUES (@ScheduleID, @CompanyID, @Name)", connectionString.SqlConnection);
                create.Parameters.AddWithValue("@ScheduleID", entity.ScheduleID);
                create.Parameters.AddWithValue("@CompanyID", entity.CompanyID);
                create.Parameters.AddWithValue("@Name", entity.Name);
                connectionString.Open();
                create.ExecuteNonQuery();
                connectionString.Dispose();
            }
        }

        public void Update(ScheduleDTO entity)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand update = new SqlCommand("UPDATE Schedule SET Name = @Name WHERE ScheduleID = @ScheduleID", connectionString.SqlConnection);
                update.Parameters.AddWithValue("@Name", entity.Name);
                update.Parameters.AddWithValue("@ScheduleID", entity.ScheduleID);
                connectionString.Open();
                update.ExecuteNonQuery();
                connectionString.Dispose();
            }
        }

        public void Delete(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand delete = new SqlCommand("DELETE FROM Schedule WHERE ScheduleID = @ScheduleID", connectionString.SqlConnection);
                delete.Parameters.AddWithValue("@ScheduleID", id);
                connectionString.Open();
            }
        }

        public ScheduleDTO GetById(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                ScheduleDTO dto = new ScheduleDTO();
                SqlCommand getSchedule = new SqlCommand("SELECT * FROM Schedule WHERE ScheduleID = @ScheduleID", connectionString.SqlConnection);
                getSchedule.Parameters.AddWithValue("@ScheduleID", id);
                connectionString.Open();
                var reader = getSchedule.ExecuteReader();
                while (reader.Read())
                {
                    dto.ScheduleID = reader.GetString(0);
                    dto.CompanyID = reader.GetString(1);
                    dto.Name = reader.GetString(2);
                }
                connectionString.Dispose();
                return dto;
            }
        }

        public List<ShiftDTO> GetShiftsFromSchedule(string id)
        {
            List<ShiftDTO> dtos = new List<ShiftDTO>();
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getShifts = new SqlCommand("SELECT * FROM Shift WHERE ScheduleID = @ScheduleID", connectionString.SqlConnection);
                getShifts.Parameters.AddWithValue("@ScheduleID", id);
                connectionString.Open();
                var reader = getShifts.ExecuteReader();
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
    }
}
