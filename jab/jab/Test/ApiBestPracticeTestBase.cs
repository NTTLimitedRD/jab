using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using jab.Fixture;
using jab.Interfaces;
using NSwag;
using NUnit.Framework;

namespace jab.tests
{
    public partial class ApiBestPracticeTestBase
    {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static ApiBestPracticeTestBase()
        {
            // Load manually to circumvent Resharper test runner issues

            using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("jab.fixtures.swagger.json"))
            using (StreamReader stringReader = new StreamReader(resourceStream))
            {
                TestDefinition = stringReader.ReadToEnd();
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApiBestPracticeTestBase()
        {
            // Do nothing
        }

        protected static IEnumerable<TestCaseData> DeleteOperations => SwaggerTestHelpers.GetOperations(TestDefinition, jabApiOperation => jabApiOperation.Method == SwaggerOperationMethod.Delete);
        protected static IEnumerable<TestCaseData> Operations => SwaggerTestHelpers.GetOperations(TestDefinition);
        protected static IEnumerable<TestCaseData> Services => SwaggerTestHelpers.GetServices(TestDefinition);

        protected static readonly string TestDefinition;

        private ILifetimeScope Container { get; set; }

        public ApiBestPracticeTestBase(ApiTestFixture fixture)
        {
            this.Container = fixture.CreateComponentContext();
            Container.InjectUnsetProperties(this);
        }
    }
}
