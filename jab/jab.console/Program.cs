using System;
using System.CodeDom;
using System.Threading;
using NUnit.Engine;
using NUnit.Common;
using NUnit.Engine.Runners;
using System.IO;
using System.Reflection;
using System.Reactive.Subjects;

namespace jab.console
{
    /// <summary>
    /// Entry point.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The number of failed tests.
        /// </summary>
        public static int FailedTestCount { get; set; }

        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int Main(string[] args)
        {
            //if (args.Length == 0 || args.Length > 2)
            //{
            //    Console.WriteLine("usage: jab.console.exe <path to swagger.json> [api url]");
            //    return 2;
            //}

            //if (args.Length > 1)
            //{
            //    Environment.SetEnvironmentVariable(EnvironmentVariables.ActiveTestsFlag, "1", EnvironmentVariableTarget.Process);
            //    Environment.SetEnvironmentVariable(EnvironmentVariables.BaseUrl, args[1], EnvironmentVariableTarget.Process);
            //}

            //string fixturesPath = Path.Combine(thisPath, "fixtures");

            //// does the fixtures folder exist?
            //if (!Directory.Exists(fixturesPath))
            //    Directory.CreateDirectory(fixturesPath);

            //// Copy the given fixture across
            //File.Copy(args[0], Path.Combine(fixturesPath, "swagger.json"), true);

            TestEventListener testEventListener;

            testEventListener = new TestEventListener();
            testEventListener.OnTestCaseResult += TestEventListener_OnTestCaseResult;
            FailedTestCount = 0;

            using (ITestEngine engine = TestEngineActivator.CreateInstance())
            using (ITestEngineRunner testRunner = new LocalTestRunner(engine.Services, new TestPackage("jab.dll")))
            {
                testRunner.Run(testEventListener, TestFilter.Empty);
            }

            return FailedTestCount > 0 ? ExitCodes.TestFailed : ExitCodes.Success;
        }

        /// <summary>
        /// Called after a test completes.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="result"></param>
        /// <param name="message"></param>
        private static void TestEventListener_OnTestCaseResult(string name, TestResult result, string message)
        {
            if(result == TestResult.Failed)
            {
                Console.Out.WriteLine($"{name} {message ?? string.Empty}");
                FailedTestCount++;
            }
        }
    }
}