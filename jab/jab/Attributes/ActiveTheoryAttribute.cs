using System;
using Xunit;
using Xunit.Sdk;

namespace jab.Attributes
{
    /// <summary>
    /// "Active test", requires a connection/instance to the actual API, does not just use Swagger.
    /// </summary>
    [XunitTestCaseDiscoverer("jab.Extensions.ActiveTheoryDiscoverer", "jab")]
    public class ActiveTheoryAttribute : TheoryAttribute { }
}
