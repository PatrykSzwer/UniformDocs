using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Engine;

namespace TestsRunner
{
    public static class TestsRunner
    {
        public static Queue<string> testsQueue = new Queue<string>();
        public static bool IsProcessing;
        // TODO: use config?
        //public static string path = "C:\\gitc\\UniformDocs\\test\\UniformDocs.Tests\\bin\\Debug\\UniformDocs.Tests.dll";
        public static string path = "";
        static TestPackage package = new TestPackage(path);
        static ITestEngine engine = TestEngineActivator.CreateInstance();

        private static ITestFilterService filterServiceInstance;

        public static ITestFilterService FilterServiceInstance
        {
            get
            {
                if (filterServiceInstance == null)
                {
                    filterServiceInstance = engine.Services.GetService<ITestFilterService>();
                }
                return filterServiceInstance;
            }
        }

        public static bool RunTest(string testClassName)
        {
            package.AddSetting("WorkDirectory", Environment.CurrentDirectory);
            ITestFilterBuilder builder = FilterServiceInstance.GetTestFilterBuilder();
            builder.AddTest(testClassName);

            var filter = builder.GetFilter();

            using (ITestRunner runner = engine.GetRunner(package))
            {
                var result = runner.Run(null, filter);

                if (result.Attributes["result"].Value.Equals("Passed"))
                {
                    return true;
                }
            }

            return false;
        }

        public static void StartProcessingTests()
        {
            Console.WriteLine("Starting processing");
            IsProcessing = true;
            int cnt = 0;

            for (var i = 0; i <= 5; i++)
            {
                if (testsQueue.Any())
                {
                    if (cnt % 2 == 0)
                    {
                        Console.WriteLine("Thread.Sleep");
                        Thread.Sleep(30 * 1000);
                    }

                    var testToRun = testsQueue.Dequeue();
                    Console.WriteLine("testToRun: " + testToRun);
                    if (!RunTest(testToRun))
                    {
                        testsQueue.Enqueue(testToRun);
                    }

                    cnt++;
                    Console.WriteLine("cnt: " + cnt);
                }
            }

            IsProcessing = false;
        }
    }
}
