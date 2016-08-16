using NSwag;
using Xunit;

namespace jab.tests
{
    public partial class ApiBestPracticeTestBase
    {
        /// <summary>
        /// Require HTTPS support.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <param name="operation"></param>
        [Theory, ParameterisedClassData(typeof(ApiServices), testDefinition)]
        public void RequireHttps(
            SwaggerService service
        )
        {
            Assert.True(service.Schemes.Contains(SwaggerSchema.Https),
                $"{service.BaseUrl} does not support HTTPS");
        }
    }
}
