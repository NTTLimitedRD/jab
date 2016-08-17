using NSwag;

namespace jab.Interfaces
{
    public interface IJabApiOperation
    {
        SwaggerService Service { get; }
        string Path { get; }
        SwaggerOperationMethod Method { get; }
        SwaggerOperation Operation { get; }
    }
}
