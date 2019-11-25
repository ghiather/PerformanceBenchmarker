using PerformanceBenchmarker.TestExcuters;
using System;
using System.Collections.Generic;

namespace PerformanceBenchmarker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Performance Benchmarker V1.0 \n");
            Startup testExcuter = new Startup();
            testExcuter.Excute(3);
            Console.ReadKey();
        }
    }
}