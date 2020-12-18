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
                    CompanyDTO dto = new CompanyDTO(reader.GetString(0), reader.GetString(1),
                        GetAllEmployees(reader.GetString(0)));
                    dtos.Add(dto);
                }
                connectionString.Dispose();
                return dtos;
            }
        }

        public CompanyDTO Create(CompanyDTO entity)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand create = new SqlCommand("INSERT INTO Company(CompanyId, CompanyName) VALUES (@CompanyId, CompanyName)", connectionString.SqlConnection);
                create.Parameters.AddWithValue("@CompanyId", entity.CompanyId);
                create.Parameters.AddWithValue("@CompanyName", entity.CompanyName);
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

        public CompanyDTO Update(CompanyDTO entity)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand update = new SqlCommand("UPDATE Company SET CompanyName = @CompanyName WHERE CompanyId = @CompanyId", connectionString.SqlConnection);
                update.Parameters.AddWithValue("@CompanyId", entity.CompanyId);
                update.Parameters.AddWithValue("@CompanyName", entity.CompanyName);
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
                SqlCommand delete = new SqlCommand("DELETE Company WHERE CompanyId = @CompanyId", connectionString.SqlConnection);
                delete.Parameters.AddWithValue("@CompanyId", id);
                connectionString.Open();
                var result = delete.ExecuteNonQuery();
                connectionString.Dispose();
                return result != 0;
            }
        }

        public CompanyDTO GetById(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getById = new SqlCommand("SELECT * FROM Company WHERE CompanyId = @CompanyId", connectionString.SqlConnection);
                getById.Parameters.AddWithValue("@CompanyId", id);
                connectionString.Open();
                var reader = getById.ExecuteReader();
                while (reader.Read())
                {
                    CompanyDTO dto = new CompanyDTO(reader.GetString(0), reader.GetString(1));
                    connectionString.Dispose();
                    return dto;
                }
                return new CompanyDTO();
            }
        }

        public CompanyDTO GetCompanyFromUser(string userId)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand get = new SqlCommand("SELECT CompanyId FROM UserCompanyLink WHERE UserID = @UserID", connectionString.SqlConnection);
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
                SqlCommand addEmployee = new SqlCommand("INSERT INTO UserCompanyLink(UserID, CompanyId) VALUES(@UserID, @CompanyId)", connectionString.SqlConnection);
                addEmployee.Parameters.AddWithValue("@UserID", userId);
                addEmployee.Parameters.AddWithValue("@CompanyId", company.CompanyId);
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
                SqlCommand getAllEmp = new SqlCommand("SELECT UserID FROM UserCompanyLink WHERE CompanyId = @CompanyId", connectionString.SqlConnection);
                getAllEmp.Parameters.AddWithValue("@CompanyId", id);
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

        public List<string> GetAllEmployeeEmails(string id)
        {
            List<string> emails = new List<string>();
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getEmails = new SqlCommand("SELECT Users.Email FROM UserCompanyLink AS Link INNER JOIN AspNetUsers AS Users ON Link.UserID = Users.Id WHERE CompanyId = @CompanyId", connectionString.SqlConnection);
                getEmails.Parameters.AddWithValue("@CompanyId", id);
                connectionString.Open();
                var reader = getEmails.ExecuteReader();
                while (reader.Read())
                {
                    emails.Add(reader.GetString(0));
                }
                connectionString.Dispose();
                return emails;
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
                SqlCommand checkEmployee = new SqlCommand("SELECT COUNT (UserID) FROM UserCompanyLink WHERE UserID = @UserID AND CompanyId = @CompanyId", connectionString.SqlConnection);
                checkEmployee.Parameters.AddWithValue("@CompanyId", companyId);
                checkEmployee.Parameters.AddWithValue("@UserID", userId);
                connectionString.Open();
                var result = (int)checkEmployee.ExecuteScalar();
                connectionString.Dispose();
                return result != 0;
            }
        }
    }
}
