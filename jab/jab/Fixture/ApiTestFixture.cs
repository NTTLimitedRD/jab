using System;
using Autofac;

namespace jab.Fixture
{
    public class ApiTestFixture
        : IDisposable
    {
        IContainer container;

        public ILifetimeScope CreateComponentContext()
        {
            var containerBuilder = new ContainerBuilder();
            container = containerBuilder.Build();
            return container.BeginLifetimeScope();
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}
