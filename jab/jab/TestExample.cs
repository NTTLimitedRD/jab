using NSwag;
using Xunit;

namespace jab
{
    class TestExample
    {
        [Theory, ClassData(typeof(ApiOperations))]
        public void ExampleCase(string path, SwaggerOperationMethod method, SwaggerOperation operation)
        {
            Assert.Equal("blue", "green");
        }
    }
}
