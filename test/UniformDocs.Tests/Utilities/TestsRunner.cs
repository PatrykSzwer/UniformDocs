using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using NUnit.Engine;

namespace UniformDocs.Tests.Utilities
{
    public static class TestsRunner
    {
        public static Queue<string> TestsQueue = new Queue<string>();
        public static bool IsProcessing;
        private static readonly string Path = Assembly.GetExecutingAssembly().Location;
        private static readonly TestPackage Package = new TestPackage(Path);
        private static readonly ITestEngine Engine = TestEngineActivator.CreateInstance();

        private static ITestFilterService _filterServiceInstance;

        public static ITestFilterService FilterServiceInstance =>
            _filterServiceInstance ??
            (_filterServiceInstance = Engine.Services.GetService<ITestFilterService>());

        public static bool RunTest(string testClassName)
        {
            Package.AddSetting("WorkDirectory", Environment.CurrentDirectory);
            ITestFilterBuilder builder = FilterServiceInstance.GetTestFilterBuilder();
            builder.AddTest(testClassName);

            var filter = builder.GetFilter();

            using (ITestRunner runner = Engine.GetRunner(Package))
            {
                var result = runner.Run(null, filter);

                if (result.Attributes != null && result.Attributes["result"].Value.Equals("Passed"))
                {
                    return true;
                }
            }

            return false;
        }

        public static void StartProcessingTests()
        {
            IsProcessing = true;
            int cnt = 0;

            for (var i = 0; i <= 5; i++)
            {
                if (TestsQueue.Any())
                {
                    if (cnt % 2 == 0)
                    {
                        Thread.Sleep(20 * 1000);
                    }

                    var testToRun = TestsQueue.Dequeue();

                    if (!RunTest(testToRun))
                    {
                        TestsQueue.Enqueue(testToRun);
                    }

                    cnt++;
                }
            }

            IsProcessing = false;
        }
    }
}
