using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace PerformanceBenchmarker.TestExcuters
{
    static class DataTableTest
    {
        public static long ExcuteTest(string connectionString, string query)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                    }
                }
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}
