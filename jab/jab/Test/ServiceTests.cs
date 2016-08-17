using NSwag;
using NUnit.Framework;

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
        [TestCaseSource(nameof(Services))]
        public void RequireHttps(SwaggerService service)
        {
            Assert.That(
                service.Schemes,
                Contains.Item(SwaggerSchema.Https));
        }
    }
}
