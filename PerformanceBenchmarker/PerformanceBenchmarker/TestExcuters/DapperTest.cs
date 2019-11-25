using System.Collections.Generic;
using System.Diagnostics;
using System.Data.SqlClient;
using Dapper;
using PerformanceBenchmarker.Models;

namespace PerformanceBenchmarker.TestExcuters
{
    public class DapperTest : ITestExcuter
    {
        private string connectionString;

        public DapperTest(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string TestTitle
        {
            get
            {
                return "Dapper";
            }
        }
        public long Get(string query)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            ExcuteQuery(query);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetById(string query, int id)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            ExcuteQuery(query, new { id = id });
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        private void ExcuteQuery(string query, object parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var list = connection.Query<List<Model>>(query, parameters);
            }
        }
    }
}
