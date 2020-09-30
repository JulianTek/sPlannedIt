using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using sPlannedIt.Data.Models;

namespace sPlannedIt.Data
{
    public class CompanyManager_Data
    {
        public static bool CreateCompany(string companyID, string name)
        {
            using (ConnectionString connectionString = new ConnectionString())
            {
                SqlCommand createCompany = new SqlCommand("INSERT INTO Company (CompanyID, CompanyName) VALUES (" + companyID + ", " + name + ")");
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
                SqlCommand findAllCompanies = new SqlCommand("SELECT * FROM Company");
                connectionString.sqlConnection.Open();
                var reader = findAllCompanies.ExecuteReader();
                while (reader.Read())
                {
                    companyDtos.Add(new CompanyDTO()
                    {
                        CompanyID = reader.GetString(0),
                        CompanyName = reader.GetString(0)
                    });
                }
                connectionString.Dispose();
                return companyDtos;
            }
        }
    }
}
