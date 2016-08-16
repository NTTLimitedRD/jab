using jab.Interfaces;
using NSwag;

namespace jab
{
    public class JabApiOperation : IJabApiOperation
    {
        public SwaggerService Service { get; set; }
        public string Path { get; set; }
        public SwaggerOperationMethod Method { get; set; }
        public SwaggerOperation Operation { get; set; }
    }
}
