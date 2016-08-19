using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace jab.Interfaces
{
    /// <summary>
    /// Configuration for running a test.
    /// </summary>
     public interface IJabTestConfiguration
    {
        /// <summary>
        /// The contents of the swagger file to test.
        /// </summary>
        string SwaggerFile { get;  }

        /// <summary>
        /// The Base URL of the web service to test or null, if 
        /// the test is just being performed on the swagger file.
        /// </summary>
        Uri BaseUrl { get; } 
    }
}
