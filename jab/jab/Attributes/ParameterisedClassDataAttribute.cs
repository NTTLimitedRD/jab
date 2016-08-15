using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace jab.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ParameterisedClassDataAttribute : DataAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDataAttribute"/> class.
        /// </summary>
        /// <param name="class">The class that provides the data.</param>
        public ParameterisedClassDataAttribute(Type @class, params object[] instanceParams)
        {
            Class = @class;
            InstanceArgs = instanceParams;
        }

        /// <summary>
        /// Gets the type of the class that provides the data.
        /// </summary>
        public Type Class { get; private set; }

        public object[] InstanceArgs { get; private set; }

        /// <inheritdoc/>
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return (IEnumerable<object[]>)Activator.CreateInstance(Class, args: InstanceArgs);
        }
    }
}
