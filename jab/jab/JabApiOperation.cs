using System.Security.Authentication.ExtendedProtection.Configuration;
using jab.Interfaces;
using NSwag;

namespace jab
{
    /// <summary>
    /// A swagger operation (i.e. web method) called by jab.
    /// </summary>
    public class JabApiOperation : IJabApiOperation
    {
        /// <summary>
        /// Create a new <see cref="JabApiOperation"/>
        /// </summary>
        /// <param name="service"></param>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <param name="operation"></param>
        public JabApiOperation(SwaggerService service, string path, SwaggerOperationMethod method,
            SwaggerOperation operation)
        {
            Service = service;
            Path = path;
            Method = method;
            Operation = operation;
        }

        /// <summary>
        /// The <see cref="SwaggerService"/>.
        /// </summary>
        public SwaggerService Service { get;  }

        /// <summary>
        /// The path (non host portion of the URL).
        /// </summary>
        public string Path { get;  }

        /// <summary>
        /// The HTTP method or verb used to call the web service.
        /// </summary>
        public SwaggerOperationMethod Method { get;  }

        /// <summary>
        /// The <see cref="SwaggerOperation"/>.
        /// </summary>
        public SwaggerOperation Operation { get;  }
    }
}
