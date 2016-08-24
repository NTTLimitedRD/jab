using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Threading;
using NUnit.Engine;
using NUnit.Engine.Runners;
using System.IO;
using System.Linq;
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
            int result;

            try
            {
                result = Parser.Default.ParseArguments<CommandLineOptions>(args)
                    .MapResult(OnRunTests, OnError);
            }
            catch (CommandLineArgumentException ex)
            {
                Console.Error.WriteLine(ex.Message);
                result = ExitCodes.BadArgument;
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
        /// <exception cref="CommandLineArgumentException">
        /// One or more arguments on the command line were invalid.
        /// </exception>
        public static int OnRunTests(CommandLineOptions commandLineOptions)
        {
            TestEventListener testEventListener;

            try
            {
                JabTestConfiguration.Register(
                    File.ReadAllText(commandLineOptions.SwaggerFilePath),
                    commandLineOptions.BaseUrl != null ? new Uri(commandLineOptions.BaseUrl) : null);
            }
            catch (IOException ex)
            {
                throw new CommandLineArgumentException(
                    $"Swagger file '{commandLineOptions.SwaggerFilePath}' does not exist or cannot be read", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new CommandLineArgumentException(
                    $"Swagger file '{commandLineOptions.SwaggerFilePath}' cannot be accessed", ex);
            }
            catch (UriFormatException ex)
            {
                throw new CommandLineArgumentException(
                    $"Base URL '{commandLineOptions.BaseUrl}' is not a valid URI", ex);
            }

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
        /// Called on command line parsing errors.
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static int OnError(IEnumerable<Error> errors)
        {
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
