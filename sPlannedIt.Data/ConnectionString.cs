using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace sPlannedIt.Data
{ 
    class ConnectionString : IDisposable
    {


        public SqlConnection SqlConnection = new SqlConnection(@"Data Source=mssql.fhict.local;User ID=dbi431603_sPlannedIt;Password=G70847 371t3!;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        public void Dispose()
        {
            SqlConnection.Close();
            SqlConnection.Dispose();
        }

        public void Open()
        {
            SqlConnection.Open();
        }
    }



    // local db @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=sPlannedItLocal;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
    // test db Data Source=mssql.fhict.local;Initial Catalog=dbi431603_sitests;User ID=dbi431603_sitests;Password=G70847 371t3!;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
}
