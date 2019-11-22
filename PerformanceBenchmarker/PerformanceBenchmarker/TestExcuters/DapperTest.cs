using System.Collections.Generic;
using System.Diagnostics;
using System.Data.SqlClient;
using Dapper;
using PerformanceBenchmarker.Models;

namespace PerformanceBenchmarker.TestExcuters
{
    public class DapperTest
    {
        public static long ExcuteTest(string connectionString, string query)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var list = connection.Query<List<Model>>(query);
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}
