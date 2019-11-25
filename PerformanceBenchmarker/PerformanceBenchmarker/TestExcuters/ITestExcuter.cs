using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceBenchmarker.TestExcuters
{
    interface ITestExcuter
    {
        string TestTitle { get; }
        long Get(string query);
        long GetById(string query, int id);
    }
}
