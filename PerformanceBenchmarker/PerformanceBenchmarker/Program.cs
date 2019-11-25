using PerformanceBenchmarker.TestExcuters;
using System;
using System.Collections.Generic;

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

            Startup testExcuter = new Startup();
            testExcuter.Excute(connectionString, query, 3);

            Console.ReadKey();
        }
    }
}