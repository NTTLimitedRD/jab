using NSwag;
using NSwag.Collections;
using System.Collections.Generic;
using System.Collections;
using jab.Interfaces;

namespace jab
{
    /// <summary>
    /// An enumeration class for API definitions
    /// </summary>
    public class ApiOperations: IEnumerable<object[]>
    {
        /// <summary>
        /// Enumerable operations (string path, SwaggerOperationMethod method, SwaggerOperation operation)
        /// for this API
        /// </summary>
        private List<object[]> _operations;

        /// <summary>
        /// Dictionary of operations available on this API
        /// </summary>
        private ObservableDictionary<string, SwaggerOperations> _paths;

        /// <summary>
        /// The swagger service definition
        /// </summary>
        private SwaggerService _service;

        /// <summary>
        /// File path to the local swagger JSON file
        /// </summary>
        private string _swaggerFilePath;

        /// <summary>
        /// Load all API operations from a swagger file.
        /// </summary>
        /// <param name="swaggerFile">Path to the swagger file</param>
        public ApiOperations(string swaggerFile)
        {
            _swaggerFilePath = swaggerFile;
            _service = SwaggerLoader.LoadServiceFromFile(_swaggerFilePath);
            _paths = _service.Paths;

            _operations = new List<object[]>();

            foreach(var path in _paths)
            {
                foreach (var operation in path.Value)
                {
                    _operations.Add(
                        new object[] {
                            new JabApiOperation {
                                Service = _service,
                                Path = path.Key,
                                Method = operation.Key,
                                Operation = operation.Value
                            }
                        }
                    );
                }
            }
        }

        /// <summary>
        /// Enumerate the API operations.
        /// </summary>
        /// <returns>Collection of possible operations (string path, SwaggerOperationMethod method, SwaggerOperation operation)</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            return _operations.GetEnumerator();
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
