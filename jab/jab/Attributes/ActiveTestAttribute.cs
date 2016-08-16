using System;

namespace jab.Attributes
{
    /// <summary>
    /// "Active test", requires a connection/instance to the actual API, does not just use Swagger.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ActiveTestAttribute : System.Attribute
    {
    }
}
