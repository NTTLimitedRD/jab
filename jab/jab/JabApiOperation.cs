using System.Security.Authentication.ExtendedProtection.Configuration;
using jab.Interfaces;
using NSwag;

namespace jab
{
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

        public SwaggerService Service { get;  }
        public string Path { get;  }
        public SwaggerOperationMethod Method { get;  }
        public SwaggerOperation Operation { get;  }
    }
}
