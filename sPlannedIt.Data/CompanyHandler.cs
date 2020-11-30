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

        public bool Create(CompanyDTO entity)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand create = new SqlCommand("INSERT INTO Company(CompanyID, CompanyName) VALUES (@CompanyID, CompanyName)", connectionString.SqlConnection);
                create.Parameters.AddWithValue("@CompanyID", entity.CompanyID);
                create.Parameters.AddWithValue("@CompanyName", entity.CompanyName);
                connectionString.Open();
                var result = create.ExecuteNonQuery();
                connectionString.Dispose();
                return result != 0;
            }
        }

        public bool Update(CompanyDTO entity)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand update = new SqlCommand("UPDATE Company SET CompanyName = @CompanyName WHERE CompanyID = @CompanyID", connectionString.SqlConnection);
                update.Parameters.AddWithValue("@CompanyID", entity.CompanyID);
                update.Parameters.AddWithValue("@CompanyName", entity.CompanyName);
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
                SqlCommand delete = new SqlCommand("DELETE Company WHERE CompanyID = @CompanyID", connectionString.SqlConnection);
                delete.Parameters.AddWithValue("@CompanyID", id);
                connectionString.Open();
                var result = delete.ExecuteNonQuery();
                connectionString.Dispose();
                return result != 0;
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

        public CompanyDTO GetCompanyFromUser(string userId)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand get = new SqlCommand("SELECT CompanyID FROM UserCompanyLink WHERE UserID = @UserID", connectionString.SqlConnection);
                get.Parameters.AddWithValue("@UserID", userId);
                connectionString.Open();
                var companyId = (string)get.ExecuteScalar();
                connectionString.Dispose();
                return GetById(companyId);
            }
        }

        public bool AddEmployee(string userId, CompanyDTO company)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand addEmployee = new SqlCommand("INSERT INTO UserCompanyLink(UserID, CompanyID) VALUES(@UserID, @CompanyID)", connectionString.SqlConnection);
                addEmployee.Parameters.AddWithValue("@UserID", userId);
                addEmployee.Parameters.AddWithValue("@CompanyID", company.CompanyID);
                connectionString.Open();
                var result = addEmployee.ExecuteNonQuery();
                connectionString.Dispose();
                return result != 0;
            }
        }

        public bool RemoveEmployee(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand removeEmployee = new SqlCommand("DELETE FROM UserCompanyLink WHERE UserID = @UserID", connectionString.SqlConnection);
                removeEmployee.Parameters.AddWithValue("@UserID", id);
                connectionString.Open();
                var result = removeEmployee.ExecuteNonQuery();
                connectionString.Dispose();
                return result != 0;
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

        public bool CheckIfCompanyNameExists(string name)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand checkName = new SqlCommand("SELECT COUNT (CompanyName) FROM Company WHERE CompanyName = @CompanyName", connectionString.SqlConnection);
                checkName.Parameters.AddWithValue("@CompanyName", name);
                connectionString.Open();
                var result = (int)checkName.ExecuteScalar();
                connectionString.Dispose();
                return result != 0;
            }
        }

        public bool CheckIfEmployeeInCompany(string userId, string companyId)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand checkEmployee = new SqlCommand("SELECT COUNT (UserID) FROM UserCompanyLink WHERE UserID = @UserID AND CompanyID = @CompanyID", connectionString.SqlConnection);
                checkEmployee.Parameters.AddWithValue("@CompanyID", companyId);
                checkEmployee.Parameters.AddWithValue("@UserID", userId);
                connectionString.Open();
                var result = (int)checkEmployee.ExecuteScalar();
                connectionString.Dispose();
                return result != 0;
            }
        }
    }
}
