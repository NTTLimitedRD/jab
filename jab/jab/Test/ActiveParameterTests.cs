using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using jab.Http;
using jab.Interfaces;
using NJsonSchema;
using NSwag;
using NUnit.Framework;

namespace jab.Test
{
    /// <summary>
    /// Base class for API tests.
    /// </summary>
    public partial class ApiBestPracticeTestBase
    {
        /// <summary>
        /// For GET methods that have parameters which are integers. try sending a really big number.
        /// </summary>
        /// <param name="operation"></param>
        [TestCaseSource(nameof(GetOperations), Category = "Active")]
        public async Task RangedIntegerParameterTesting(IJabApiOperation operation)
        {
            if (operation.Operation.ActualParameters.Any(
                    parameter => parameter.Kind == SwaggerParameterKind.Query && parameter.Type == JsonObjectType.Integer)
                    &&
                operation.Method == SwaggerOperationMethod.Get)
            {
                HttpClient client = Configuration.GetClient();
                SwaggerParameter parameter = operation.Operation.Parameters.First(p => p.Type == JsonObjectType.Integer && p.Kind == SwaggerParameterKind.Query);
                                  
                HttpResponseMessage results = await client.GetAsync(operation.Path + "?" + parameter.Name + "=" + UInt64.MaxValue.ToString());
                Assert.That(
                    results.StatusCode,
                    Is.EqualTo(HttpStatusCode.InternalServerError));
            }
        }
    }
}
