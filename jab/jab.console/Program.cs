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

            using (ITestEngine engine = TestEngineActivator.CreateInstance())
            using (ITestEngineRunner testRunner = new LocalTestRunner(engine.Services, new TestPackage("jab.dll")))
            {
                TestEventListener testEventListener;

                testEventListener = new TestEventListener();
                testRunner.Run(testEventListener, TestFilter.Empty);
            }

            return result;
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