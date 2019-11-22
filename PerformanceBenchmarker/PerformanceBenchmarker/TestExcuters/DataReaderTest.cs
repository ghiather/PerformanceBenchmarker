using PerformanceBenchmarker.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace PerformanceBenchmarker.TestExcuters
{
    static class DataReaderTest
    {
        public static long ExcuteTest(string connectionString, string query)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            IList<Model> list = new List<Model>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        list.Add(new Model()
                        {
                            id = (long)dataReader["id"],
                            a = (string)dataReader["a"],
                            b = (string)dataReader["b"]
                        });
                    }
                    connection.Close();
                }
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}
