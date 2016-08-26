using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using jab.Interfaces;
using NSwag;
using NUnit.Framework;

namespace jab.Test
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

        /// <summary>
        /// Register components.
        /// </summary>
        /// <param name="swaggerFile">
        /// The contents of the swagger file. This cannot be null, empty or whitespace.
        /// </param>
        /// <param name="baseUrl">
        /// The optional base URL to use for testing the web service.
        /// </param>
        public static void Register(string swaggerFile, Uri baseUrl = null)
        {
            if (string.IsNullOrWhiteSpace(swaggerFile))
            {
                throw new ArgumentNullException(nameof(swaggerFile));
            }

            Configuration = new JabTestConfiguration(swaggerFile, baseUrl);
        }
    }
}
