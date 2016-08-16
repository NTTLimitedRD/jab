using System;
using System.Threading;
using Xunit.Runners;
using System.IO;
using System.Reflection;
using System.Reactive.Subjects;

namespace jab.console
{
    class Program
    {
        // We use consoleLock because messages can arrive in parallel, so we want to make sure we get
        // consistent console output.
        static Subject<string> output = new Subject<string>();
        static Subject<string> errorOutput = new Subject<string>();

        // Use an event to know when we're done
        static ManualResetEvent finished = new ManualResetEvent(false);

        // Start out assuming success; we'll set this to 1 if we get a failed test
        static int result = 0;

        static int Main(string[] args)
        {
            if (args.Length == 0 || args.Length > 2)
            {
                Console.WriteLine("usage: jab.console.exe <path to swagger.json> [api url]");
                return 2;
            }

            if (args.Length > 1)
            {
                Environment.SetEnvironmentVariable(Constants.active_tests_flag, "1", EnvironmentVariableTarget.Process);
                Environment.SetEnvironmentVariable(Constants.base_url_env, args[1], EnvironmentVariableTarget.Process);
            }

            string thisPath = Assembly.GetExecutingAssembly().GetDirectoryPath();
            string fixturesPath = Path.Combine(thisPath, "fixtures");

            // does the fixtures folder exist?
            if (!Directory.Exists(fixturesPath))
                Directory.CreateDirectory(fixturesPath);

            // Copy the given fixture across
            File.Copy(args[0], Path.Combine(fixturesPath, "swagger.json"), true);

            var testAssembly = Path.Combine(thisPath, "jab.dll");
            var typeName = typeof(jab.tests.ApiBestPracticeTestBase).FullName;

            output.Subscribe(msg => Console.WriteLine(msg));

            errorOutput.Subscribe(errMsg =>
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(errMsg);

                Console.ResetColor();
            });

            using (var runner = AssemblyRunner.WithAppDomain(testAssembly))
            {
                runner.OnDiscoveryComplete = OnDiscoveryComplete;
                runner.OnExecutionComplete = OnExecutionComplete;
                runner.OnTestFailed = OnTestFailed;
                runner.OnTestSkipped = OnTestSkipped;

                Console.WriteLine("Discovering...");
                runner.Start(typeName);

                finished.WaitOne();
                finished.Dispose();

                Console.ReadKey();

                return result;
            }
        }

        static void OnDiscoveryComplete(DiscoveryCompleteInfo info)
        {
            output.OnNext($"Running {info.TestCasesToRun} of {info.TestCasesDiscovered} tests...");
        }

        static void OnExecutionComplete(ExecutionCompleteInfo info)
        {
            output.OnNext($"Finished: {info.TotalTests} tests in {Math.Round(info.ExecutionTime, 3)}s ({info.TestsFailed} failed, {info.TestsSkipped} skipped)");

            finished.Set();
        }

        static void OnTestFailed(TestFailedInfo info)
        {            
            errorOutput.OnNext(String.Format("[FAIL] {0}: {1}", info.TestDisplayName, info.ExceptionMessage));
            if (info.ExceptionStackTrace != null)
                errorOutput.OnNext(info.ExceptionStackTrace);

            result = 1;
        }

        static void OnTestSkipped(TestSkippedInfo info)
        {
            output.OnNext(String.Format("[SKIP] {0}: {1}", info.TestDisplayName, info.SkipReason));
        }
    }

    public static class AssemblyExtensions {
        public static string GetDirectoryPath(this Assembly assembly)
        {
            string filePath = new Uri(assembly.CodeBase).LocalPath;
            return Path.GetDirectoryName(filePath);
        }
    }
}