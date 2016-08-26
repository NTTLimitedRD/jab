using NSwag;
using NUnit.Framework;

namespace jab.Test
{
    public partial class ApiBestPracticeTestBase
    {
        /// <summary>
        /// Require HTTPS support.
        /// </summary>
        /// <param name="service">
        /// The <see cref="SwaggerService"/> to test.
        /// </param>
        [TestCaseSource(nameof(ApiBestPracticeTestBase.Services))]
        public void RequireHttps(SwaggerService service)
        {
            Assert.That(
                service.Schemes,
                Contains.Item(SwaggerSchema.Https));
        }
    }
}
