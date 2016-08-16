using NSwag;
using Xunit;

namespace jab.tests
{
    public partial class ApiBestPracticeTestBase
    {
        /// <summary>
        /// Require HTTPS support.
        /// </summary>
        /// <param name="service">
        /// The <see cref="SwaggerService"/> to test.
        /// </param>
        [Theory, ParameterisedClassData(typeof(ApiServices), testDefinition)]
        public void RequireHttps(SwaggerService service)
        {
            Assert.True(service.Schemes.Contains(SwaggerSchema.Https),
                $"{service.BaseUrl} does not support HTTPS");
        }
    }
}
