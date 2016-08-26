using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Threading;
using NUnit.Engine;
using NUnit.Engine.Runners;
using System.IO;
using System.Linq;
using CommandLine;
using ApiBestPracticeTestBase = Jab.Test.ApiBestPracticeTestBase;

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
            int result;

            try
            {
                result =  Parser.Default.ParseArguments<CommandLineOptions>(args)
                                .MapResult(OnRunTests, OnError);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                result = ExitCodes.Unknown;
            }

            return result;
        }

        /// <summary>
        /// Run the tests.
        /// </summary>
        /// <param name="commandLineOptions"></param>
        /// <returns></returns>
        public static int OnRunTests(CommandLineOptions commandLineOptions)
        {
            TestEventListener testEventListener;

            // TODO: Better command line error validation

            testEventListener = new TestEventListener();
            testEventListener.OnTestCaseResult += TestEventListener_OnTestCaseResult;
            FailedTestCount = 0;

            ApiBestPracticeTestBase.Register(
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
        /// Called on command line parsing errors.
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static int OnError(IEnumerable<Error> errors)
        {
            // Current formatting sucks
            // Console.Error.WriteLine(errors.First().ToString());

            Console.Error.WriteLine("usage: jab.console.exe <path to swagger.json> [-u <api url>]");
            return ExitCodes.BadArgument;
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
            if(message != null)
            {
                FailedTestCount++;
                Console.Out.WriteLine($"{FailedTestCount:000}: {name} {message.Substring(0, message.IndexOf('\n'))}");
            }
        }
    }
}
