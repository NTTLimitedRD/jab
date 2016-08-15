using NSwag;
using NSwag.Collections;
using System.Collections.Generic;
using System;
using System.Collections;

namespace jab
{
    public class ApiOperations: IEnumerable<object[]>
    {
        private List<object[]> _operations;
        private ObservableDictionary<string, SwaggerOperations> _paths;
        private SwaggerService _service;
        private string _swaggerFilePath;

        public ApiOperations(string swaggerFile)
        {
            _swaggerFilePath = swaggerFile;
            _service = SwaggerLoader.LoadServiceFromFile(_swaggerFilePath);
            _paths = _service.Paths;

            var operations = new List<object[]>();

            foreach(var path in _paths)
            {
                foreach (var operation in path.Value)
                {
                    _operations.Add( new object[] {
                        path.Key,
                        operation.Key,
                        operation.Value
                    }
                    );
                }
                
            }
            _operations = operations;
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return _operations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
