using System.Data.SqlClient;
using System.Diagnostics;
using PerformanceBenchmarker.Helpers;
using PerformanceBenchmarker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace PerformanceBenchmarker.TestExcuters
{
    public class FastMemberTest : ITestExcuter
    {
        private string connectionString;

        public FastMemberTest(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string TestTitle
        {
            get
            {
                return "FastMember";
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
            List<SqlParameter> parameters = new List<SqlParameter>()
            { new SqlParameter("@id", SqlDbType.Int) {Value = id} };
            ExcuteQuery(query, parameters);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }


        private void ExcuteQuery(string query, List<SqlParameter> parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null && parameters.Count > 0)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    var list = FastMemberHelper.ToList<Model>(dataReader);
                }
            }
        }
    }
}