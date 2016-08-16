using System;
using System.Collections.Generic;
using System.Linq;
using jab.Attributes;
using Xunit;
using jab.Interfaces;
using NSwag;

namespace jab.tests
{
    public partial class ApiBestPracticeTestBase
    {
        [ActiveTheory]
        [Theory, ApiOperationsData(testDefinition)]
        public void RangedIntegerParameterTesting(IJabApiOperation operation)
        {
            if (operation.Operation.ActualParameters.Any(
                    parameter => parameter.Kind == SwaggerParameterKind.Query)){

            }
        }
    }
}
