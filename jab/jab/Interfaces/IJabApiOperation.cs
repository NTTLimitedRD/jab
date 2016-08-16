using NSwag;

namespace jab.Interfaces
{
    public interface IJabApiOperation
    {
        SwaggerService Service { get; set; }
        string Path { get; set; }
        SwaggerOperationMethod Method { get; set; }
        SwaggerOperation Operation { get; set; }
    }
}
