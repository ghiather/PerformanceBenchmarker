using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceBenchmarker.TestExcuters
{
    public class Startup
    {
        public void Excute(string connectionString, string query, int n)
        {
            List<ITestExcuter> testExcuters = new List<ITestExcuter>();
            init(testExcuters);

            foreach (ITestExcuter testExcuter in testExcuters)
            {
                for (int i = 1; i <= n; i++)
                {
                    Console.WriteLine(string.Format("{0} Test #{1}: {2}",
                        testExcuter.TestTitle,
                        i,
                        testExcuter.ExcuteTest(connectionString, query)));
                }
                AddLine();
            }
        }

        private void init(List<ITestExcuter> testExcuters)
        {
            testExcuters.Add(new DataTableTest());
            testExcuters.Add(new FastMemberTest());
            testExcuters.Add(new DataReaderTest());
            testExcuters.Add(new DapperTest());
        }

        private void AddLine()
        {
            Console.WriteLine("-----------------------------------------");
        }
    }
}
