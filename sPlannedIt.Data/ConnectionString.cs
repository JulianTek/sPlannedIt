using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace sPlannedIt.Data
{
    class ConnectionString : IDisposable
    {
        public SqlConnection sqlConnection = new SqlConnection
        (
            // local db @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=sPlannedItLocal;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=sPlannedItLocal;Integrated Security=True;MultipleActiveResultSets=True"
        );

        public void Dispose()
        {
            sqlConnection.Close();
            sqlConnection.Dispose();
        }
    }
}
