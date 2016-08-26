using NSwag;

namespace Jab.Interfaces
{
    /// <summary>
    /// A swagger operation (i.e. web method) called by jab.
    /// </summary>
    public interface IJabApiOperation
    {

        /// <summary>
        /// The <see cref="SwaggerService"/>.
        /// </summary>
        SwaggerService Service { get; }

        /// <summary>
        /// The path (non host portion of the URL).
        /// </summary>
        string Path { get; }

        /// <summary>
        /// The HTTP method or verb used to call the web service.
        /// </summary>
        SwaggerOperationMethod Method { get; }

        /// <summary>
        /// The <see cref="SwaggerOperation"/>.
        /// </summary>
        SwaggerOperation Operation { get; }
    }
}
