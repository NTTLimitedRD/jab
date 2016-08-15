using NSwag;

namespace jab
{
    public static class SwaggerLoader
    {
        public static SwaggerService LoadServiceFromFile(string file)
        {
            return SwaggerService.FromFile(file);
        }
    }
}
