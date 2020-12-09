using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Data.SqlClient;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Data
{
    public class ScheduleHandler : IScheduleHandler
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
                    ScheduleDTO dto = new ScheduleDTO(reader.GetString(0), reader.GetString(1), reader.GetString(2));
                    dtos.Add(dto);
                }
                connectionString.Dispose();
                return dtos;
            }
        }

        public bool Create(ScheduleDTO entity)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand create = new SqlCommand("INSERT INTO Schedule(ScheduleId, CompanyId, Name) VALUES (@ScheduleId, @CompanyId, @Name)", connectionString.SqlConnection);
                create.Parameters.AddWithValue("@ScheduleId", entity.ScheduleId);
                create.Parameters.AddWithValue("@CompanyId", entity.CompanyId);
                create.Parameters.AddWithValue("@Name", entity.Name);
                connectionString.Open();
                var result = create.ExecuteNonQuery();
                connectionString.Dispose();
                return result != 0;
            }
        }

        public bool Update(ScheduleDTO entity)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand update = new SqlCommand("UPDATE Schedule SET Name = @Name WHERE ScheduleId = @ScheduleId", connectionString.SqlConnection);
                update.Parameters.AddWithValue("@Name", entity.Name);
                update.Parameters.AddWithValue("@ScheduleId", entity.ScheduleId);
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
                SqlCommand delete = new SqlCommand("DELETE FROM Schedule WHERE ScheduleId = @ScheduleId", connectionString.SqlConnection);
                delete.Parameters.AddWithValue("@ScheduleId", id);
                connectionString.Open();
                var result = delete.ExecuteNonQuery();
                connectionString.Dispose();
                return result != 0;
            }
        }

        public ScheduleDTO GetById(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {

                SqlCommand getSchedule = new SqlCommand("SELECT * FROM Schedule WHERE ScheduleId = @ScheduleId", connectionString.SqlConnection);
                getSchedule.Parameters.AddWithValue("@ScheduleId", id);
                connectionString.Open();
                var reader = getSchedule.ExecuteReader();
                while (reader.Read())
                {
                    ScheduleDTO dto = new ScheduleDTO(reader.GetString(2), reader.GetString(0), reader.GetString(1));
                    connectionString.Dispose();
                    return dto;
                }
                connectionString.Dispose();
                return new ScheduleDTO();
            }
        }

        public List<ShiftDTO> GetShiftsFromSchedule(string id)
        {
            List<ShiftDTO> dtos = new List<ShiftDTO>();
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getShifts = new SqlCommand("SELECT * FROM Shift WHERE ScheduleId = @ScheduleId", connectionString.SqlConnection);
                getShifts.Parameters.AddWithValue("@ScheduleId", id);
                connectionString.Open();
                var reader = getShifts.ExecuteReader();
                while (reader.Read())
                {
                    ShiftDTO dto = new ShiftDTO(reader.GetString(0), reader.GetString(1),
                        reader.GetString(5), reader.GetDateTime(4), reader.GetInt32(2) , reader.GetInt32(3));
                    dtos.Add(dto);
                }
                connectionString.Dispose();
                return dtos;
            }
        }

        public List<ShiftDTO> GetTodaysShifts(string id, DateTime date)
        {
            List<ShiftDTO> dtos = new List<ShiftDTO>();
            List<ScheduleDTO> scheduleDtos = GetSchedulesFromCompany(id);
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getTodayShifts = new SqlCommand("SELECT * FROM Shift WHERE Date = @Date", connectionString.SqlConnection);
                getTodayShifts.Parameters.AddWithValue("@Date", date.Date);
                connectionString.Open();
                var reader = getTodayShifts.ExecuteReader();
                while (reader.Read())
                {
                    foreach (ScheduleDTO sched in scheduleDtos)
                    {
                        if (sched.ScheduleId == reader.GetString(1))
                        {
                            ShiftDTO dto = new ShiftDTO(reader.GetString(0), reader.GetString(1),
                                reader.GetString(5), reader.GetDateTime(4), reader.GetInt32(2), reader.GetInt32(3));
                            dtos.Add(dto);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                connectionString.Dispose();
                return dtos;
            }
        }

        public List<ScheduleDTO> GetSchedulesFromCompany(string id)
        {
            List<ScheduleDTO> dtos = new List<ScheduleDTO>();
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getSchedules = new SqlCommand("SELECT * From Schedule WHERE CompanyId = @CompanyId", connectionString.SqlConnection);
                getSchedules.Parameters.AddWithValue("@CompanyId", id);
                connectionString.Open();
                var reader = getSchedules.ExecuteReader();
                while (reader.Read())
                {
                    ScheduleDTO dto = GetById(reader.GetString(0));
                    dtos.Add(dto);
                }
                connectionString.Dispose();
                return dtos;
            }
        }
    }
}
