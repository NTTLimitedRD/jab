using System.Linq;
using jab.Attributes;
using jab.Interfaces;
using NSwag;

namespace jab.tests
{
    public partial class ApiBestPracticeTestBase
    {
        [ActiveTheory]
        [ApiOperationsData(testDefinition)]
        public void RangedIntegerParameterTesting(IJabApiOperation operation)
        {
            if (operation.Operation.ActualParameters.Any(
                    parameter => parameter.Kind == SwaggerParameterKind.Query)){

            }
        }
    }
}
