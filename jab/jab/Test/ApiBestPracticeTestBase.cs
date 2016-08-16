using Autofac;
using jab.Fixture;
using Xunit;

namespace jab.tests
{
    public partial class ApiBestPracticeTestBase
        : IClassFixture<ApiTestFixture>
    {
        const string testDefinition = "fixtures/swagger.json";

        private ILifetimeScope Container { get; set; }

        public ApiBestPracticeTestBase(ApiTestFixture fixture)
        {
            this.Container = fixture.CreateComponentContext();
            Container.InjectUnsetProperties(this);
        }
    }
}
