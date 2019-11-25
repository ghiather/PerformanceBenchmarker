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

            Startup testExcuter = new Startup();
            testExcuter.Excute(3);

            Console.ReadKey();
        }
    }
}