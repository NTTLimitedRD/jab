using jab.Attributes;
using NSwag;
using Xunit;
using System.Linq;

namespace jab
{
    public class TestExample
    {
        [Theory, ParameterisedClassData(typeof(ApiOperations), "samples/example.json")]
        public void DeleteMethodsShouldNotTakeFormEncodedData(
            SwaggerService service,
            string path, 
            SwaggerOperationMethod method, 
            SwaggerOperation operation)
        {
            if (method == SwaggerOperationMethod.Delete)
            {
                Assert.Null(operation.ActualConsumes);
            } else
            {
                Assert.True(true);
            }
        }
    }
}
