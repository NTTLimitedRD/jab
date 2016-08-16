using NSwag;
using Xunit;
using System.Linq;
using jab.Interfaces;

namespace jab.tests
{
    public partial class ApiBestPracticeTestBase
    {
        const string testDefinition = "fixtures/swagger.json";

        [Theory, ParameterisedClassData(typeof(ApiOperations), testDefinition)]
        public void DeleteMethodsShouldNotTakeFormEncodedData(
            IJabApiOperation operation)
        {
            if (operation.Method == SwaggerOperationMethod.Delete)
            {
                Assert.Null(operation.Operation.ActualConsumes);
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
        public void NoApiKeysInQueryParameters(
            IJabApiOperation operation)
        {
            if (operation.Operation.ActualParameters.Count > 0)
            {
                Assert.False(
                    operation.Operation.Parameters.Count(
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
