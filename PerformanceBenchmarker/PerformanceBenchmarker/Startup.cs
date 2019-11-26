using PerformanceBenchmarker.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace PerformanceBenchmarker.TestExcuters
{
    public class Startup
    {
        public void Excute()
        {
            List<ITestExcuter> testExcuters = new List<ITestExcuter>();
            InitializeExcuters(testExcuters);
            var getQuery = "SELECT [id], [a], [b]  FROM [Test].[dbo].[t1]";
            var joinQuery = "SELECT t1.id, t1.a, t1.b  FROM t1 JOIN t2 ON t1.id = t2.id";
            var getByIdQuery = "SELECT [id], [a], [b]  FROM [Test].[dbo].[t1] WHERE id = @id";
            int numberOfRuns = 2;

            ExcuteQuery(testExcuters, getQuery, numberOfRuns, QueryType.Get);
            ExcuteQuery(testExcuters, joinQuery, numberOfRuns, QueryType.Join);
            ExcuteQuery(testExcuters, getByIdQuery, numberOfRuns, QueryType.GetById, 3);

        }

        private void ExcuteQuery(List<ITestExcuter> testExcuters, string query, int numberOfRuns, QueryType queryType, int? id = null)
        {
            long restul = 0;
            foreach (ITestExcuter testExcuter in testExcuters)
            {

                for (int i = 1; i <= numberOfRuns; i++)
                {
                    switch (queryType)
                    {
                        case QueryType.Get:
                        case QueryType.Join:
                            {
                                restul = testExcuter.Get(query);
                                break;
                            }
                        case QueryType.GetById:
                            {
                                restul = testExcuter.GetById(query, (int)id);
                                break;
                            }
                    }
                    Console.WriteLine(string.Format("{0} {1} #{2}: {3}",
                        testExcuter.TestTitle,
                        queryType.ToString(),
                        i,
                        restul));
                }
                AddLine();
            }
        }

        private void InitializeExcuters(List<ITestExcuter> testExcuters)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["PerformanceBenchmarker"].ConnectionString;
            testExcuters.Add(new DataTableTest(connectionString));
            testExcuters.Add(new FastMemberTest(connectionString));
            testExcuters.Add(new DataReaderTest(connectionString));
            testExcuters.Add(new DapperTest(connectionString));
        }

        private void AddLine()
        {
            Console.WriteLine("-----------------------------------------");
        }
    }
}
