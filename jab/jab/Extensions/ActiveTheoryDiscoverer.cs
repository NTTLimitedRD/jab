using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;
using Xunit.Abstractions;

namespace jab.Extensions
{
    public class ActiveTheoryDiscoverer : IXunitTestCaseDiscoverer
    {
        readonly IMessageSink diagnosticMessageSink;
        readonly TheoryDiscoverer theoryDiscoverer;

        public ActiveTheoryDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;

            theoryDiscoverer = new TheoryDiscoverer(diagnosticMessageSink);
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            var defaultMethodDisplay = discoveryOptions.MethodDisplayOrDefault();
            var envVar = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process);

            if (envVar.Contains(Constants.active_tests_flag))
            {

                // Unlike fact discovery, the underlying algorithm for theories is complex, so we let the theory discoverer
                // do its work, and do a little on-the-fly conversion into our own test cases.
                return theoryDiscoverer.Discover(discoveryOptions, testMethod, factAttribute)
                                   .Select(testCase => testCase is XunitTheoryTestCase
                                                           ? (IXunitTestCase)new ActiveTheoryTestCase(diagnosticMessageSink, defaultMethodDisplay, testCase.TestMethod)
                                                           : new ActiveFactTestCase(diagnosticMessageSink, defaultMethodDisplay, testCase.TestMethod, testCase.TestMethodArguments));
            } else
            {
                return new IXunitTestCase[] { };
            }
        }
    }
}
