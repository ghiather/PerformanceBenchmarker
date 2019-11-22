using PerformanceBenchmarker.TestExcuters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceBenchmarker
{
    class Program
    {
        private static IList<long> DataTableResults, FastMemberResults, DapperResults, DataReaderResults;

        static void Main(string[] args)
        {
            Console.WriteLine("Performance Benchmarker V1.0 \n");
            DataTableResults = new List<long>();
            FastMemberResults = new List<long>();
            DapperResults = new List<long>();
            DataReaderResults = new List<long>();

            var connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True";
            var query = "SELECT [id], [a], [b]  FROM [Test].[dbo].[t1]";

            Excute(connectionString, query);
        }

        private static void Excute(string connectionString, string query)
        {
            for (int i = 1; i <= 3; i++)
            {
                DataReaderResults.Add(DataReaderTest.ExcuteTest(connectionString, query));
                DataTableResults.Add(DataTableTest.ExcuteTest(connectionString, query));
                FastMemberResults.Add(FastMemberTest.ExcuteTest(connectionString, query));
                DapperResults.Add(DapperTest.ExcuteTest(connectionString, query));
            }

            foreach (var item in DataTableResults.Select((value, i) => new { i, value }))
            {
                Console.WriteLine(String.Format("DataTable Test #{0}: {1}", item.i, item.value));
            }

            Console.WriteLine();

            foreach (var item in FastMemberResults.Select((value, i) => new { i, value }))
            {
                Console.WriteLine(String.Format("FastMember Test #{0}: {1}", item.i, item.value));
            }

            Console.WriteLine();

            foreach (var item in DapperResults.Select((value, i) => new { i, value }))
            {
                Console.WriteLine(String.Format("Dapper Test #{0}: {1}", item.i, item.value));
            }

            Console.WriteLine();

            foreach (var item in DataReaderResults.Select((value, i) => new { i, value }))
            {
                Console.WriteLine(String.Format("DataReader Test #{0}: {1}", item.i, item.value));
            }
            Console.ReadKey();
        }
    }

}