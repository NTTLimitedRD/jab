using System;
using System.Collections.Generic;
using System.IO;
using NSwag;
using NUnit.Framework;
using System.Linq;
using jab.Interfaces;

namespace jab.tests
{
    public partial class ApiBestPracticeTestBase
    {
        private const string FormEncodedFormat = "application/x-www-form-urlencoded";
        private const string MultiPartFormFormat = "multipart/form-data";

        // Standard web service formats
        private static readonly List<string> StandardFormats = new List<string>
        {
            FormEncodedFormat,
            MultiPartFormFormat,
            "application/xml",
            "application/json"
        };

        /// <summary>
        /// Operations using the "DELETE" verb should not accept form encoded data.
        /// </summary>
        /// <param name="operation"></param>
        [TestCaseSource(nameof(DeleteOperations))]
        public void DeleteMethodsShouldNotTakeFormEncodedData(IJabApiOperation operation)
        {
            Assume.That(operation,
                Has.Property("Method").EqualTo(SwaggerOperationMethod.Delete));
            Assert.That(
                operation.Operation,
                Has.Property("Consumes").Null
                    .Or.Property("Consumes").Not.Contains(FormEncodedFormat)
                    .And.Property("Consumes").Not.Contains(MultiPartFormFormat),
                $"{operation.Path} has 'DELETE' verb but accepts data");
        }

        /// <summary>
        /// Use the "DELETE" verb for delete or removal operations.
        /// </summary>
        /// <param name="operation"></param>
        [TestCaseSource(nameof(DeleteOperations))]
        public void UseDeleteVerbForDelete(IJabApiOperation operation)
        {
            List<string> deleteSynonyms = new List<string>
            {
                "delete",
                "remove"
            };

            Assume.That(operation,
                Has.Property("Method").EqualTo(SwaggerOperationMethod.Delete));
            Assert.That(
                    operation,
                    Has.Property("Path").Not.SubsetOf(deleteSynonyms),
                    $"{operation.Path} should use 'DELETE' verb instead of '{operation.Method}'");
        }

        /// <summary>
        /// Do not include secrets in query parameters. These get logged or included in browser history.
        /// <para></para>
        /// Similar to https://www.owasp.org/index.php/REST_Security_Cheat_Sheet#Authentication_and_session_management.
        /// </summary>
        /// <param name="operation"></param>
        [TestCaseSource(nameof(Operations))]
        public void NoSecretsInQueryParameters(IJabApiOperation operation)
        {
            List<string> secretSynonyms = new List<string>
            {
                "password",
                "secret",
                "key"
            };

            IList<SwaggerParameter> queryParametersContainingSecrets =
                new List<SwaggerParameter>(operation.Operation.ActualParameters.Where(
                    parameter => parameter.Kind == SwaggerParameterKind.Query
                                 && secretSynonyms.Any(term => parameter.Name.IndexOf(term, 0, StringComparison.InvariantCultureIgnoreCase) != -1)));

            Assert.That(
                queryParametersContainingSecrets,
                Is.Not.Null.And.Empty,
                $"Parameters: {(string.Join(", ", queryParametersContainingSecrets.Select(p => p.Name)))}");
        }

        /// <summary>
        /// Do not include secrets in query parameters. These get logged or included in browser history.
        /// </summary>
        /// <param name="operation"></param>
        [TestCaseSource(nameof(Operations))]
        public void NoNonStandardProductFormats(IJabApiOperation operation)
        {
            IList<string> nonStandardFormats =
                operation.Operation?.Produces?.Where(product => !StandardFormats.Contains(product)).ToList();

            Assert.That(
                nonStandardFormats,
                Is.Null.Or.Empty,
                $"Nonstandard formats: {(string.Join(", ", nonStandardFormats ?? new string[0]))}");
        }

        /// <summary>
        /// Do not include secrets in query parameters. These get logged or included in browser history.
        /// </summary>
        /// <param name="operation"></param>
        [TestCaseSource(nameof(Operations))]
        public void NoNonStandardConsumptionFormats(IJabApiOperation operation)
        {
            IList<string> nonStandardFormats =
                operation.Operation?.Consumes?.Where(consumption => !StandardFormats.Contains(consumption)).ToList();

            Assert.That(
                nonStandardFormats,
                Is.Null.Or.Empty,
                $"Nonstandard formats: {(string.Join(", ", nonStandardFormats ?? new string[0]))}");
        }
    }
}
