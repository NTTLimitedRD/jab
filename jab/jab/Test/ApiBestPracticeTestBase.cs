using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using jab.Interfaces;
using NSwag;
using NUnit.Framework;

namespace jab.tests
{
    public partial class ApiBestPracticeTestBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        static ApiBestPracticeTestBase()
        {
            ContainerBuilder containerBuilder;
            string swaggerFile;

            using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("jab.fixtures.swagger.json"))
            using (StreamReader stringReader = new StreamReader(resourceStream))
            {
                swaggerFile = stringReader.ReadToEnd();
            }

            containerBuilder = new ContainerBuilder();
            JabTestConfiguration.Register(containerBuilder, swaggerFile, null);

            Container = containerBuilder.Build();
        }

        /// <summary>
        /// Operations using the DELETE HTTP verb.
        /// </summary>
        protected static IEnumerable<TestCaseData> DeleteOperations => 
            SwaggerTestHelpers.GetOperations(
                Container.Resolve<IJabTestConfiguration>(), 
                jabApiOperation => jabApiOperation.Method == SwaggerOperationMethod.Delete);

        /// <summary>
        /// Operations.
        /// </summary>
        protected static IEnumerable<TestCaseData> Operations => SwaggerTestHelpers.GetOperations(Container.Resolve<IJabTestConfiguration>());

        /// <summary>
        /// Services.
        /// </summary>
        protected static IEnumerable<TestCaseData> Services => SwaggerTestHelpers.GetServices(Container.Resolve<IJabTestConfiguration>());

        /// <summary>
        /// The container used to pass configuration to the test class.
        /// </summary>
        public static ILifetimeScope Container { get; set; }
    }
}
