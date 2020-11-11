using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;
using sPlannedIt.Entities.DTOs;
using sPlannedIt.Interface.DAL;

namespace sPlannedIt.Data
{
    public class CompanyHandler : ICompanyHandler
    {
        public List<CompanyDTO> GetAll()
        {
            List<CompanyDTO> dtos = new List<CompanyDTO>();
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getAll = new SqlCommand("SELECT * FROM Company", connectionString.SqlConnection);
                connectionString.Open();
                var reader = getAll.ExecuteReader();
                while (reader.Read())
                {
                    CompanyDTO dto = new CompanyDTO()
                    {
                        CompanyID = reader.GetString(0),
                        CompanyName = reader.GetString(1),
                        Employees = GetAllEmployees(reader.GetString(0))
                    };
                    dtos.Add(dto);
                }
                connectionString.Dispose();
                return dtos;
            }
        }

        public void Create(CompanyDTO entity)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand create = new SqlCommand("INSERT INTO Company(CompanyID, CompanyName) VALUES (@CompanyID, CompanyName)", connectionString.SqlConnection);
                create.Parameters.AddWithValue("@CompanyID", entity.CompanyID);
                create.Parameters.AddWithValue("@CompanyName", entity.CompanyName);
                connectionString.Open();
                create.ExecuteNonQuery();
                connectionString.Dispose();
            }
        }

        public void Update(CompanyDTO entity)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand update = new SqlCommand("UPDATE Company SET CompanyName = @CompanyName WHERE CompanyID = @CompanyID", connectionString.SqlConnection);
                update.Parameters.AddWithValue("@CompanyID", entity.CompanyID);
                update.Parameters.AddWithValue("@CompanyName", entity.CompanyName);
                connectionString.Open();
                update.ExecuteNonQuery();
                connectionString.Dispose();
            }
        }

        public void Delete(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand delete = new SqlCommand("DELETE Company WHERE CompanyID = @CompanyID", connectionString.SqlConnection);
                delete.Parameters.AddWithValue("@CompanyID", id);
                connectionString.Open();
                delete.ExecuteNonQuery();
                connectionString.Dispose();
            }
        }

        public CompanyDTO GetById(string id)
        {
            CompanyDTO dto = new CompanyDTO();
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getById = new SqlCommand("SELECT * FROM Company WHERE CompanyID = @CompanyID", connectionString.SqlConnection);
                getById.Parameters.AddWithValue("@CompanyID", id);
                connectionString.Open();
                var reader = getById.ExecuteReader();
                while (reader.Read())
                {
                    dto.CompanyID = reader.GetString(0);
                    dto.CompanyName = reader.GetString(1);
                    dto.Employees = GetAllEmployees(reader.GetString(0));
                }
                connectionString.Dispose();
                return dto;
            }
        }

        public void AddEmployee(UserDTO user, CompanyDTO company)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand addEmployee = new SqlCommand("INSERT INTO UserCompanyLink(UserID, CompanyID) VALUES(@UserID, @CompanyID)", connectionString.SqlConnection);
                addEmployee.Parameters.AddWithValue("@UserID", user.UserId);
                addEmployee.Parameters.AddWithValue("@CompanyID", company.CompanyID);
                connectionString.Open();
                addEmployee.ExecuteNonQuery();
                connectionString.Dispose();
            }
        }

        public void RemoveEmployee(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand removeEmployee = new SqlCommand("DELETE FROM UserCompanyLink WHERE UserID = @UserID", connectionString.SqlConnection);
                removeEmployee.Parameters.AddWithValue("@UserID", id);
                connectionString.Open();
                removeEmployee.ExecuteNonQuery();
                connectionString.Dispose();
            }
        }

        public List<string> GetAllEmployees(string id)
        {
            List<string> ids = new List<string>();
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getAllEmp = new SqlCommand("SELECT UserID FROM UserCompanyLink WHERE CompanyID = @CompanyID", connectionString.SqlConnection);
                getAllEmp.Parameters.AddWithValue("@CompanyID", id);
                connectionString.Open();
                var reader = getAllEmp.ExecuteReader();
                while (reader.Read())
                {
                    ids.Add(reader.GetString(0));
                }
                connectionString.Dispose();
                return ids;
            }
        }

        public void RemoveAllEmployees(List<string> ids)
        {
            foreach (string id in ids)
            {
                RemoveEmployee(id);
            }
        }
    }
}
