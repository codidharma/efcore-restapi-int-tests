namespace WebApi.IntegrationTests.Setup;

[CollectionDefinition(nameof(IntegrationTestCollection))]
public class IntegrationTestCollection : ICollectionFixture<IntegrationTestsWebAppFactory>
{
}
