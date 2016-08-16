using System;
using NSwag;
using NSwag.Collections;
using System.Collections.Generic;
using System.Collections;

namespace jab
{
    /// <summary>
    /// An enumeration class for API services.
    /// </summary>
    public class ApiServices : IEnumerable<object[]>
    {
        /// <summary>
        /// Enumerable operations (string path, SwaggerOperationMethod method, SwaggerOperation operation)
        /// for this API
        /// </summary>
        private List<object[]> _services;

        /// <summary>
        /// File path to the local swagger JSON file
        /// </summary>
        private string _swaggerFilePath;

        /// <summary>
        /// Load all API operations from a swagger file.
        /// </summary>
        /// <param name="swaggerFile">Path to the swagger file</param>
        public ApiServices(string swaggerFile)
        {
            _swaggerFilePath = swaggerFile;
            _services = new List<object[]>
                {
                    new object[]
                    {
                        SwaggerLoader.LoadServiceFromFile(_swaggerFilePath)
                    }
                };
        }

        /// <summary>
        /// Enumerate the API operations.
        /// </summary>
        /// <returns>Collection of possible operations (string path, SwaggerOperationMethod method, SwaggerOperation operation)</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            return _services.GetEnumerator();
        }

        /// <summary>
        /// Enumerate the API operations.
        /// </summary>
        /// <returns>A collection enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}