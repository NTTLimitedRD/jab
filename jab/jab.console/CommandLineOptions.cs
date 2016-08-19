using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace jab.console
{
    public class CommandLineOptions
    {
        [ValueOption(0)]
        public string SwaggerFilePath { get; set; }

        [Option('u', "baseUrl", DefaultValue = null, Required = false)]
        public string BaseUrl { get; set; }
    }
}
