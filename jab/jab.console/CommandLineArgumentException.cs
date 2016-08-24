using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace jab.console
{
    /// <summary>
    /// Thrown for an invalid command line argument.
    /// </summary>
    public class CommandLineArgumentException : Exception
    {
        /// <summary>
        /// Create a new <see cref="CommandLineArgumentException"/>.
        /// </summary>
        public CommandLineArgumentException()
        {
            // Do nothing
        }

        /// <summary>
        /// Create a new <see cref="CommandLineArgumentException"/>.
        /// </summary>
        public CommandLineArgumentException(string message) 
            : base(message)
        {
            // Do nothing
        }

        /// <summary>
        /// Create a new <see cref="CommandLineArgumentException"/>.
        /// </summary>
        public CommandLineArgumentException(string message, Exception innerException) 
            : base(message, innerException)
        {
            // Do nothing
        }

        /// <summary>
        /// Create a new <see cref="CommandLineArgumentException"/>.
        /// </summary>
        protected CommandLineArgumentException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
            // Do nothing
        }
    }
}
