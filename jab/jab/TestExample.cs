using jab.Attributes;
using NSwag;
using Xunit;

namespace jab
{
    public class TestExample
    {
        [Theory, ParameterisedClassData(typeof(ApiOperations), "samples/example.json")]
        public void ExampleCase(string path, SwaggerOperationMethod method, SwaggerOperation operation)
        {
            Assert.Equal("blue", "green");
        }
    }
}
