using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using Testcontainers.MsSql;
using WebApi.Data;

namespace WebApi.IntegrationTests.Setup;

public class IntegrationTestsWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _container;
    private readonly string _databaseConnectionString;
    private DbConnection _dbConnection = null!;
    
    public ExpenseManagerContext ExpenseManagerContext { get; private set; } = null!;
    

    public IntegrationTestsWebAppFactory()
    {
        _container = MsSqlContainerFactory.Create();
        _databaseConnectionString = _container.GetConnectionString();
        
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveDbContext<ExpenseManagerContext>();
            services.AddDbContext<ExpenseManagerContext>(options => 
            {
                options.UseSqlServer(_databaseConnectionString);
            });
            services.EnsureDbCreated<ExpenseManagerContext>();
        });
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        ExpenseManagerContext = Services.CreateScope().ServiceProvider.GetRequiredService<ExpenseManagerContext>();
        _dbConnection = ExpenseManagerContext.Database.GetDbConnection();
        await _dbConnection.OpenAsync();
    }

    public async new Task DisposeAsync()
    {
        await _dbConnection.CloseAsync();
        await _container.DisposeAsync();
    }
}
