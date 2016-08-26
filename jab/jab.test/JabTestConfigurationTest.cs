using System;
using NUnit.Framework;

namespace Jab.Test
{
    [TestFixture]
    public class JabTestConfigurationTest
    {
        public readonly string SwaggerJson = 
@"{
    'swagger': '2.0',
    'paths': {}
}";

        [Test]
        public void Ctor_NullBaseUrl()
        {
            JabTestConfiguration jabTestConfiguration;

            jabTestConfiguration = new JabTestConfiguration(SwaggerJson, null);

            Assert.That(
                jabTestConfiguration,
                Has.Property("SwaggerService").Not.Null
                   .And.Property("SwaggerService").Property("Paths").Empty);
            Assert.That(
                jabTestConfiguration,
                Has.Property("BaseUrl").Null);
        }

        [Test]
        public void Ctor_SuppliedBaseUrl()
        {
            JabTestConfiguration jabTestConfiguration;
            Uri baseUrl;

            baseUrl = new Uri("http://myapi.com");

            jabTestConfiguration = new JabTestConfiguration(SwaggerJson, baseUrl);

            Assert.That(
                jabTestConfiguration,
                Has.Property("SwaggerService").Not.Null
                   .And.Property("SwaggerService").Property("Paths").Empty);
            Assert.That(
                jabTestConfiguration,
                Has.Property("BaseUrl").EqualTo(baseUrl));
        }

        [Test]
        public void Ctor_NullSwaggerJson()
        {
            Assert.That(
                () => new JabTestConfiguration(null, null),
                Throws.ArgumentException.And.Property("ParamName").EqualTo("swaggerJson"));
        }

        [Test]
        public void Ctor_EmtpySwaggerJson()
        {
            Assert.That(
                () => new JabTestConfiguration("", null),
                Throws.ArgumentException.And.Property("ParamName").EqualTo("swaggerJson"));
        }

        [Test]
        public void Ctor_WhitespaceSwaggerJson()
        {
            Assert.That(
                () => new JabTestConfiguration(" ", null),
                Throws.ArgumentException.And.Property("ParamName").EqualTo("swaggerJson"));
        }
    }
}
