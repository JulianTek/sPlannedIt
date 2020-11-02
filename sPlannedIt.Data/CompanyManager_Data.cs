using System;
using System.Collections.Generic;
using System.Text;
using sPlannedIt.Data.Models;
using Microsoft.Data.SqlClient;

namespace sPlannedIt.Data
{
    public class CompanyManager_Data
    {
        public static bool CreateCompany(string companyID, string name)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand createCompany = new SqlCommand("INSERT INTO Company (CompanyID, CompanyName) VALUES (@CompanyID, @CompanyName)", connectionString.SqlConnection);
                createCompany.Parameters.AddWithValue("@CompanyID", companyID);
                createCompany.Parameters.AddWithValue("@CompanyName", name);
                connectionString.SqlConnection.Open();
                var result = createCompany.ExecuteNonQuery();
                connectionString.Dispose();
                if (result != 0)
                {
                    return true;
                }

                return false;
            }
        }

        public static List<CompanyDTO> FindAllCompanies()
        {
            List<CompanyDTO> companyDtos = new List<CompanyDTO>();
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand findAllCompanies = new SqlCommand("SELECT * FROM Company", connectionString.SqlConnection);
                connectionString.SqlConnection.Open();
                var reader = findAllCompanies.ExecuteReader();
                while (reader.Read())
                {
                    companyDtos.Add(new CompanyDTO()
                    {
                        CompanyID = reader.GetString(0),
                        CompanyName = reader.GetString(1),
                        Employees = new List<string>()
                    });
                }
                connectionString.Dispose();
                return companyDtos;
            }
        }

        public static CompanyDTO FindCompanyDto(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                CompanyDTO companyDto = new CompanyDTO()
                {
                    Employees = new List<string>()
                };
                SqlCommand findCompany = new SqlCommand("SELECT * FROM Company WHERE @CompanyID = CompanyID", connectionString.SqlConnection);
                findCompany.Parameters.AddWithValue("@CompanyID", id);
                connectionString.SqlConnection.Open();
                var reader = findCompany.ExecuteReader();
                while (reader.Read())
                {
                    companyDto.CompanyID = reader.GetString(0);
                    companyDto.CompanyName = reader.GetString(1);
                }
                connectionString.Dispose();
                return companyDto;
            }
        }

        public static string GetEmployee(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getEmployee = new SqlCommand("SELECT Email FROM AspNetUsers WHERE @Id = Id", connectionString.SqlConnection);
                getEmployee.Parameters.AddWithValue("@Id", id);
                connectionString.SqlConnection.Open();
                string result = getEmployee.ExecuteScalar().ToString();
                connectionString.Dispose();
                return result;
            }
        }

        public static int CheckIfEmployeeIsInCompany(string userId, string companyId)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand checkEmployee = new SqlCommand("SELECT COUNT(companyID) FROM UserCompanyLink WHERE @companyID = companyID AND @userID = userID", connectionString.SqlConnection);
                checkEmployee.Parameters.AddWithValue("@userID", userId);
                checkEmployee.Parameters.AddWithValue("@companyID", companyId);
                connectionString.SqlConnection.Open();
                var result = (int)checkEmployee.ExecuteScalar();
                connectionString.Dispose();
                return result;
            }
        }

        public static int AddEmployeeToCompany(string userId, string companyId)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand addEmployee = new SqlCommand("INSERT INTO UserCompanyLink (userID, companyID) VALUES (@userID, @companyID)", connectionString.SqlConnection);
                addEmployee.Parameters.AddWithValue("@userID", userId);
                addEmployee.Parameters.AddWithValue("@companyID", companyId);
                connectionString.SqlConnection.Open();
                Console.WriteLine(addEmployee.CommandText);
                int result = addEmployee.ExecuteNonQuery();
                connectionString.Dispose();
                return result;
            }
        }

        public static int RemoveEmployeeFromCompany(string userId, string companyId)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand removeEmployee = new SqlCommand("DELETE FROM UserCompanyLink WHERE @userID = userID AND @companyID = companyID", connectionString.SqlConnection);
                removeEmployee.Parameters.AddWithValue("@userID", userId);
                removeEmployee.Parameters.AddWithValue("@companyID", companyId);
                connectionString.SqlConnection.Open();
                var result = removeEmployee.ExecuteNonQuery();
                connectionString.Dispose();
                return result;
            }
        }

        public static List<string> GetEmployees(string companyId)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                List<string> userIds = new List<string>();
                SqlCommand getEmployees = new SqlCommand("SELECT userID FROM UserCompanyLink WHERE @companyID = companyID", connectionString.SqlConnection);
                getEmployees.Parameters.AddWithValue("@companyID", companyId);
                connectionString.SqlConnection.Open();
                var reader = getEmployees.ExecuteReader();
                while (reader.Read())
                {
                    userIds.Add(reader.GetString(0));
                }
                connectionString.Dispose();
                return userIds;
            }
        }

        public static void EditCompany(string id, string name)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand edit = new SqlCommand("UPDATE Company SET CompanyName = @CompanyName WHERE @CompanyID = CompanyID", connectionString.SqlConnection);
                edit.Parameters.AddWithValue("@CompanyID", id);
                edit.Parameters.AddWithValue("@CompanyName", name);
                connectionString.SqlConnection.Open();
                edit.ExecuteNonQuery();
                connectionString.Dispose();
            }
        }

        public static void DeleteCompany(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand delete = new SqlCommand("DELETE FROM Company WHERE @CompanyID = CompanyID", connectionString.SqlConnection);
                delete.Parameters.AddWithValue("@CompanyID", id);
                connectionString.SqlConnection.Open();
                delete.ExecuteNonQuery();
                connectionString.Dispose();
            }
        }

        public static string GetRoleFromEmployee(string userId)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand GetRole = new SqlCommand("SELECT RoleId from AspNetUserRoles WHERE @UserId = UserId", connectionString.SqlConnection);
                GetRole.Parameters.AddWithValue("@UserId", userId);
                connectionString.SqlConnection.Open();
                var result = (string)GetRole.ExecuteScalar();
                connectionString.Dispose();
                return result;
            }
        }

        public static int CheckIfCompanyNameExists()
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand checkName = new SqlCommand("SELECT COUNT(CompanyName) FROM Company", connectionString.SqlConnection);
                connectionString.SqlConnection.Open();
                var result = (int)checkName.ExecuteScalar();
                connectionString.Dispose();
                return result;
            }
        }
    }
}
