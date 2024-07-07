using WebApi.Data;

namespace WebApi.IntegrationTests.Setup;

[Collection(nameof(IntegrationTestCollection))]
abstract class IntegrationTestBaseClass(IntegrationTestsWebAppFactory factory) : IAsyncLifetime
{
    protected readonly ExpenseManagerContext ExpenseManagerContext = factory.ExpenseManagerContext;
    protected readonly HttpClient HttpClient = factory.CreateClient();

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
        
    }
}
