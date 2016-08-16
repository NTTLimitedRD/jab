using System;

namespace jab
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ApiOperationsDataAttribute : ParameterisedClassDataAttribute
    {
        public ApiOperationsDataAttribute(string swaggerFilePath)
            : base(typeof(ApiOperations), swaggerFilePath) { }
    }
}
