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


    }
}
