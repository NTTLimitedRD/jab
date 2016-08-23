using System;
using System.CodeDom;
using System.Threading;
using NUnit.Engine;
using NUnit.Engine.Runners;
using System.IO;
using CommandLine;
using jab.tests;

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
        public static uint FailedTestCount { get; set; }

        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args">
        /// Command line arguments.
        /// </param>
        /// <returns>
        /// The process exit code.
        /// </returns>
        public static int Main(string[] args)
        {
            CommandLineOptions commandLineOptions;
            TestEventListener testEventListener;

            commandLineOptions = new CommandLineOptions();
            if (!Parser.Default.ParseArguments(args, commandLineOptions))
            {
                Console.Error.WriteLine("usage: jab.console.exe <path to swagger.json> [-u <api url>]");
                return ExitCodes.BadArgument;
            }

            // TODO: Better command line error validation

            testEventListener = new TestEventListener();
            testEventListener.OnTestCaseResult += TestEventListener_OnTestCaseResult;
            FailedTestCount = 0;

            JabTestConfiguration.Register(
                File.ReadAllText(commandLineOptions.SwaggerFilePath),
                commandLineOptions.BaseUrl != null ? new Uri(commandLineOptions.BaseUrl) : null);

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
        /// <param name="name">
        /// The test name.
        /// </param>
        /// <param name="result">
        /// Whether the test passed or failed.
        /// </param>
        /// <param name="message">
        /// The message, usually only supplied when a test fails.
        /// </param>
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
