﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using jab.Http;
using jab.Interfaces;
using jab.tests;
using NSwag;

namespace jab
{
    /// <summary>
    /// Configuration for a Jab Test, allowing data to be passed on the command line or from other sources.
    /// </summary>
    public class JabTestConfiguration: IJabTestConfiguration
    {
        /// <summary>
        /// Create a new <see cref="JabTestConfiguration"/>.
        /// </summary>
        /// <param name="swaggerJson">
        /// The Swagger file to use for the test. This cannot be null, empty or whitespace.
        /// </param>
        /// <param name="baseUrl">
        /// An optional base URL to test against.
        /// </param>
        public JabTestConfiguration(string swaggerJson, Uri baseUrl)
        {
            if (string.IsNullOrWhiteSpace(swaggerJson))
            {
                throw new ArgumentException("Cannot be null, empty or whitespace", nameof(swaggerJson));
            }

            SwaggerService = SwaggerService.FromJson(swaggerJson);
            BaseUrl = baseUrl;
        }

        /// <summary>
        /// The <see cref="SwaggerService"/> used for testing.
        /// </summary>
        public SwaggerService SwaggerService { get; }

        /// <summary>
        /// The optional URL of the web service to test.
        /// </summary>
        public Uri BaseUrl { get; }

        /// <summary>
        /// Register components.
        /// </summary>
        /// <param name="swaggerFile">
        /// The contents of the swagger file. This cannot be null, empty or whitespace.
        /// </param>
        /// <param name="baseUrl">
        /// The optional base URL to use for testing the web service.
        /// </param>
        public static void Register(string swaggerFile, Uri baseUrl = null)
        {
            if (string.IsNullOrWhiteSpace(swaggerFile))
            {
                throw new ArgumentNullException(nameof(swaggerFile));
            }

            ApiBestPracticeTestBase.Configuration = new JabTestConfiguration(swaggerFile, baseUrl);
        }
    }
}
