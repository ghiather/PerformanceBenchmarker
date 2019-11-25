using System.Data.SqlClient;
using System.Diagnostics;
using PerformanceBenchmarker.Helpers;
using PerformanceBenchmarker.Models;

namespace PerformanceBenchmarker.TestExcuters
{
    public class FastMemberTest : ITestExcuter
    {
        public string TestTitle
        {
            get
            {
                return "FastMember";
            }
        }

        public long ExcuteTest(string connectionString, string query)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    var list = FastMemberHelper.ToList<Model>(dataReader);
                }
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}