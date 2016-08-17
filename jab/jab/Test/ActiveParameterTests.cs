using System.Linq;
using jab.Attributes;
using jab.Interfaces;
using NSwag;
using Autofac;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace jab.tests
{
    public partial class ApiBestPracticeTestBase
    {
        /// <summary>
        /// For GET methods that have parameters which are integers. try sending a really big number.
        /// </summary>
        /// <param name="operation"></param>
        [ActiveTheory]
        [ApiOperationsData(testDefinition)]
        public async Task RangedIntegerParameterTesting(IJabApiOperation operation)
        {
            if (operation.Operation.ActualParameters.Any(
                    parameter => parameter.Kind == SwaggerParameterKind.Query && parameter.Type == NJsonSchema.JsonObjectType.Integer)
                    &&
                operation.Method == SwaggerOperationMethod.Get)
            {
                var client = this.Container.Resolve<HttpClient>();
                var parameter = operation.Operation.Parameters.First(p => p.Type == NJsonSchema.JsonObjectType.Integer && p.Kind == SwaggerParameterKind.Query);
                                  
                var results = await client.GetAsync(operation.Path + "?" + parameter.Name + "=" + ulong.MaxValue.ToString());
                Assert.False(results.StatusCode != System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
