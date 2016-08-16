using System;
using Autofac;
using System.Net.Http;
using jab.Http;

namespace jab.Fixture
{
    public class ApiTestFixture
        : IDisposable
    {
        IContainer container;

        public ILifetimeScope CreateComponentContext()
        {
            var containerBuilder = new ContainerBuilder();

            // Use the environment variables to achieve this.
            // Yuck.
            var envVar = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process);

            if (envVar.Contains(Constants.active_tests_flag))
            {
                if (envVar.Contains(Constants.base_url_env)) {
                    containerBuilder
                        .Register<HttpClient>(
                            client => JabHttpClientFactory.GetClient((string)envVar[Constants.base_url_env]))
                        .As<HttpClient>();
                }
            }
            
            container = containerBuilder.Build();
            return container.BeginLifetimeScope();
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}
