using System;
using NSwag;
using NSwag.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading;
using jab.Interfaces;
using NUnit.Framework;

namespace jab
{
    /// <summary>
    /// An enumeration class for API definitions
    /// </summary>
    public static class SwaggerTestHelpers
    {
        /// <summary>
        /// Enumerate the API operations.
        /// </summary>
        /// <returns>Collection of possible operations (string path, SwaggerOperationMethod method, SwaggerOperation operation)</returns>
        public static IEnumerable<TestCaseData> GetOperations(string swaggerFilePath, Predicate<IJabApiOperation> predicate = null)
        {
            SwaggerService swaggerService;
            IJabApiOperation jabApiOperation;

            swaggerService = SwaggerLoader.LoadServiceFromFile(swaggerFilePath);

            foreach (KeyValuePair<string, SwaggerOperations> path in swaggerService.Paths)
            {
                foreach (KeyValuePair<SwaggerOperationMethod, SwaggerOperation> operation in path.Value)
                {
                    jabApiOperation = new JabApiOperation(swaggerService, path.Key, operation.Key, operation.Value);

                    if (predicate == null || predicate(jabApiOperation))
                    {
                        yield return new TestCaseData(jabApiOperation)
                            .SetName($"{operation.Key.ToString().ToUpper()} {swaggerService.BaseUrl}{path.Key}");
                    }
                }
            }
        }

        /// <summary>
        /// Enumerate the API operations.
        /// </summary>
        /// <returns>A collection enumerator</returns>
        public static IEnumerable<TestCaseData> GetServices(string swaggerFilePath)
        {
            SwaggerService swaggerService;

            swaggerService = SwaggerLoader.LoadServiceFromFile(swaggerFilePath);

            yield return new TestCaseData(swaggerService)
                .SetName(swaggerService.BaseUrl);
        }
    }
}
