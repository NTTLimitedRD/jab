using jab.Interfaces;
using NUnit.Framework;
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
        [TestCaseSource(nameof(DeleteOperations))]
        public void DeleteMethodsMustContainIdAsKeyParameter(IJabApiOperation apiOperation)
        {
            Assume.That(apiOperation.Method,
                Is.EqualTo(NSwag.SwaggerOperationMethod.Delete));
            Assert.That(
                apiOperation,
                Has.Property("Parameters").Property("Parameters").None.Property("Name").EqualTo("id"),
                    $"Must not pass ID parameter");
        }
    }
}
