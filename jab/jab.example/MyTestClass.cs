using jab.Interfaces;
using Xunit;
using System.Linq;
using jab.Fixture;

namespace jab.example
{
    public class MyTestClass
        : tests.ApiBestPracticeTestBase
    {
        public MyTestClass(ApiTestFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// DELETE operations should always contain a ID parameter.
        /// </summary>
        /// <param name="apiOperation"></param>
        [Theory, ApiOperationsData("fixtures/swagger.json")]
        public void DeleteMethodsMustContainIdAsKeyParameter(IJabApiOperation apiOperation)
        {
            if (apiOperation.Method == NSwag.SwaggerOperationMethod.Delete)
            {
                Assert.True(apiOperation.Operation.Parameters.Count(p => p.Name == "id") > 0);
            }
        }
    }
}
