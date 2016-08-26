using System.Linq;
using NUnit.Framework;
using Jab.Interfaces;
using Jab.Test;

namespace Jab.Example
{
    public class MyTestClass
        : ApiBestPracticeTestBase
    {
        /// <summary>
        /// DELETE operations should always contain a ID parameter.
        /// </summary>
        /// <param name="apiOperation"></param>
        [TestCaseSource(nameof(DeleteOperations))]
        public void DeleteMethodsMustContainIdAsKeyParameter(IJabApiOperation apiOperation)
        {
            Assume.That(
                apiOperation.Method,
                Is.EqualTo(NSwag.SwaggerOperationMethod.Delete));
            Assert.That(
                apiOperation,
                Has.Property("Operation").Property("Parameters").None.Property("Name").EqualTo("id"),
                    "Must not pass ID parameter");
        }
    }
}
