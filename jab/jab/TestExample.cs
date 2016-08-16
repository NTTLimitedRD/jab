using jab.Attributes;
using NSwag;
using Xunit;
using System.Linq;

namespace jab
{
    public partial class TestExample
    {
        const string testDefinition = "samples/example.json";

        [Theory, ParameterisedClassData(typeof(ApiOperations), testDefinition)]
        public void DeleteMethodsShouldNotTakeFormEncodedData(
            SwaggerService service,
            string path, 
            SwaggerOperationMethod method, 
            SwaggerOperation operation)
        {
            if (method == SwaggerOperationMethod.Delete)
            {
                Assert.Null(operation.ActualConsumes);
            } else
            {
                Assert.True(true);
            }
        }

        /// <summary>
        /// You should not ask for api keys in query parameters. 
        /// https://www.owasp.org/index.php/REST_Security_Cheat_Sheet#Authentication_and_session_management
        /// </summary>
        /// <param name="service"></param>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <param name="operation"></param>
        [Theory, ParameterisedClassData(typeof(ApiOperations), testDefinition)]
        public void NoApiKeysInParameters(
            SwaggerService service,
            string path,
            SwaggerOperationMethod method,
            SwaggerOperation operation)
        {
            if (operation.ActualParameters.Count > 0)
            {
                Assert.False(
                    operation.Parameters.Count(
                        c => ((c.Name.ToLower() == "apikey") || (c.Name.ToLower() == "api_key")) 
                            && c.Kind == SwaggerParameterKind.Query) > 0);
            }
            else
            {
                Assert.True(true);
            }
        }
    }
}
