using PerformanceBenchmarker.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace PerformanceBenchmarker.TestExcuters
{
    public class DataReaderTest : ITestExcuter
    {
        private string connectionString;

        public DataReaderTest(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string TestTitle
        {
            get
            {
                return "DataReader";
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
            List<Model> list = new List<Model>();
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
        }
    }
}
