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
                SqlCommand createCompany = new SqlCommand("INSERT INTO Company (CompanyID, CompanyName) VALUES (@CompanyID, @CompanyName)", connectionString.sqlConnection);
                createCompany.Parameters.AddWithValue("@CompanyID", companyID);
                createCompany.Parameters.AddWithValue("@CompanyName", name);
                connectionString.sqlConnection.Open();
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
                SqlCommand findAllCompanies = new SqlCommand("SELECT * FROM Company", connectionString.sqlConnection);
                connectionString.sqlConnection.Open();
                var reader = findAllCompanies.ExecuteReader();
                while (reader.Read())
                {
                    companyDtos.Add(new CompanyDTO()
                    {
                        CompanyID = reader.GetString(0),
                        CompanyName = reader.GetString(1)
                    });
                }
                connectionString.Dispose();
                return companyDtos;
            }
        }

        public static string GetEmployee(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand getEmployee = new SqlCommand("SELECT Email FROM AspNetUsers WHERE @Id = Id", connectionString.sqlConnection);
                getEmployee.Parameters.AddWithValue("@Id", id);
                connectionString.sqlConnection.Open();
                string result = getEmployee.ExecuteScalar().ToString();
                connectionString.Dispose();
                return result;
            }
        }

        public static int CheckIfEmployeeIsInCompany(string userId, string companyId)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand checkEmployee = new SqlCommand("SELECT COUNT(companyID) FROM UserCompanyLink WHERE @companyID = companyID AND @userID = userID", connectionString.sqlConnection);
                checkEmployee.Parameters.AddWithValue("@userID", userId);
                checkEmployee.Parameters.AddWithValue("@companyID", companyId);
                connectionString.sqlConnection.Open();
                var result = (int)checkEmployee.ExecuteScalar();
                connectionString.Dispose();
                return result;
            }
        }

        public static int AddEmployeeToCompany(string userId, string companyId)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand addEmployee = new SqlCommand("INSERT INTO UserCompanyLink (userID, companyID) VALUES (@userID, @companyID)", connectionString.sqlConnection);
                addEmployee.Parameters.AddWithValue("@userID", userId);
                addEmployee.Parameters.AddWithValue("@companyID", companyId);
                connectionString.sqlConnection.Open();
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
                SqlCommand removeEmployee = new SqlCommand("DELETE FROM UserCompanyLink WHERE @userID = userID AND @companyID = companyID", connectionString.sqlConnection);
                removeEmployee.Parameters.AddWithValue("@userID", userId);
                removeEmployee.Parameters.AddWithValue("@companyID", companyId);
                connectionString.sqlConnection.Open();
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
                SqlCommand getEmployees = new SqlCommand("SELECT userID FROM UserCompanyLink WHERE @companyID = companyID", connectionString.sqlConnection);
                getEmployees.Parameters.AddWithValue("@companyID", companyId);
                connectionString.sqlConnection.Open();
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
                SqlCommand edit = new SqlCommand("UPDATE Company SET CompanyName = @CompanyName WHERE @CompanyID = CompanyID", connectionString.sqlConnection);
                edit.Parameters.AddWithValue("@CompanyID", id);
                edit.Parameters.AddWithValue("@CompanyName", name);
                connectionString.sqlConnection.Open();
                edit.ExecuteNonQuery();
                connectionString.Dispose();
            }
        }

        public static void DeleteCompany(string id)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand delete = new SqlCommand("DELETE FROM Company WHERE @CompanyID = CompanyID", connectionString.sqlConnection);
                delete.Parameters.AddWithValue("@CompanyID", id);
                connectionString.sqlConnection.Open();
                delete.ExecuteNonQuery();
                connectionString.Dispose();
            }
        }
    }
}
