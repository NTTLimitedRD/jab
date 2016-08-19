using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using jab.Http;
using jab.Interfaces;

namespace jab
{
    /// <summary>
    /// Configuration for a Jab Test, allowing data to be passed on the command line or from other sources.
    /// </summary>
    public class JabTestConfiguration: IJabTestConfiguration
    {
        /// <summary>
        /// Create a new <see cref="JabConfiguration"/>.
        /// </summary>
        /// <param name="swaggerFile">
        /// The Swagger file to use for the test. This cannot be null, empty or whitespace.
        /// </param>
        /// <param name="baseUrl">
        /// An optional base URL to test against.
        /// </param>
        public JabTestConfiguration(string swaggerFile, Uri baseUrl)
        {
            if (string.IsNullOrWhiteSpace(swaggerFile))
            {
                throw new ArgumentNullException(nameof(swaggerFile));
            }

            SwaggerFile = swaggerFile;
            BaseUrl = baseUrl;
        }

        public string SwaggerFile { get; }

        public Uri BaseUrl { get; }

        /// <summary>
        /// Register components.
        /// </summary>
        /// <param name="containerBuilder">
        /// The <see cref="ComponentBuilder"/> to use. This cannot be null.
        /// </param>
        /// <param name="swaggerFile">
        /// The contents of the swagger file. This cannot be null, empty or whitespace.
        /// </param>
        /// <param name="baseUrl">
        /// The optional base URL to use for testing the web service.
        /// </param>
        public static void Register(ContainerBuilder containerBuilder, string swaggerFile, Uri baseUrl = null)
        {
            if (containerBuilder == null)
            {
                throw new ArgumentNullException(nameof(containerBuilder));
            }
            if (string.IsNullOrWhiteSpace(swaggerFile))
            {
                throw new ArgumentNullException(nameof(swaggerFile));
            }

            containerBuilder
                .Register(componentContext => new JabTestConfiguration(swaggerFile, baseUrl))
                .As<IJabTestConfiguration>();
            if (baseUrl != null)
            {
                containerBuilder
                    .Register(componentContext => JabHttpClientFactory.GetClient(baseUrl.ToString()))
                    .As<HttpClient>();
            }
        }
    }
}
