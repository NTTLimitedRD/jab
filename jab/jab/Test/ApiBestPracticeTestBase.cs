using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            string swaggerFile;

            using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("jab.Fixtures.swagger.json"))
            using (StreamReader stringReader = new StreamReader(resourceStream))
            {
                swaggerFile = stringReader.ReadToEnd();
            }

            Configuration = new JabTestConfiguration(swaggerFile, null);
        }

        /// <summary>
        /// Operations using the DELETE HTTP verb.
        /// </summary>
        protected static IEnumerable<TestCaseData> DeleteOperations => 
            SwaggerTestHelpers.GetOperations(
                Configuration, 
                jabApiOperation => jabApiOperation.Method == SwaggerOperationMethod.Delete);

        /// <summary>
        /// Operations.
        /// </summary>
        protected static IEnumerable<TestCaseData> Operations => SwaggerTestHelpers.GetOperations(Configuration);

        /// <summary>
        /// Services.
        /// </summary>
        protected static IEnumerable<TestCaseData> Services => SwaggerTestHelpers.GetServices(Configuration);

        /// <summary>
        /// The container used to pass configuration to the test class.
        /// </summary>
        public static IJabTestConfiguration Configuration { get; set; }
    }
}
