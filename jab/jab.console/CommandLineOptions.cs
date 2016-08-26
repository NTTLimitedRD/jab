using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace Jab.Console
{
    /// <summary>
    /// Command line options.
    /// </summary>
    public class CommandLineOptions
    {
        /// <summary>
        /// Swagger file path.
        /// </summary>
        [Value(0, Required = true, HelpText = "Swagger file.", MetaValue = "<swagger_file.json>")]
        public string SwaggerFilePath { get; set; }

        /// <summary>
        /// Optional base URL.
        /// </summary>
        [Option('u', "baseUrl", Required = false, HelpText = "Web API test URL.", MetaValue = "<url>")]
        public string BaseUrl { get; set; }
    }
}
