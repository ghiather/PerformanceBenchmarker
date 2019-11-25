using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace PerformanceBenchmarker.TestExcuters
{
    class DataTableTest : ITestExcuter
    {
        private string connectionString;

        public DataTableTest(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public string TestTitle
        {
            get
            {
                return "DataTable";
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
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                    }
                }
            }
        }
    }
}
