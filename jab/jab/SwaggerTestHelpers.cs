using System;
using NSwag;
using NSwag.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading;
using Jab.Interfaces;
using NUnit.Framework;

namespace Jab
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
        public static IEnumerable<TestCaseData> GetOperations(IJabTestConfiguration configurationSource, Predicate<IJabApiOperation> predicate = null)
        {
            IJabApiOperation jabApiOperation;

            foreach (KeyValuePair<string, SwaggerOperations> path in configurationSource.SwaggerService.Paths)
            {
                foreach (KeyValuePair<SwaggerOperationMethod, SwaggerOperation> operation in path.Value)
                {
                    jabApiOperation = new JabApiOperation(configurationSource.SwaggerService, path.Key, operation.Key, operation.Value);

                    if (predicate == null || predicate(jabApiOperation))
                    {
                        yield return new TestCaseData(jabApiOperation)
                            .SetName($"{operation.Key.ToString().ToUpper()} {configurationSource.SwaggerService.BaseUrl}{path.Key}");
                    }
                }
            }
        }

        /// <summary>
        /// Enumerate the API operations.
        /// </summary>
        /// <returns>A collection enumerator</returns>
        public static IEnumerable<TestCaseData> GetServices(IJabTestConfiguration configurationSource)
        {
            yield return new TestCaseData(configurationSource.SwaggerService)
                .SetName(configurationSource.SwaggerService.BaseUrl);
        }
    }
}
