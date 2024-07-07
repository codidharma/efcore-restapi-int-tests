using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.IntegrationTests.Setup;

public static class ServiceCollectionExtensions
{
    public static void RemoveDbContext<T>(this IServiceCollection services) where T : DbContext
    {
        var serviceDescriptor = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<T>));

        if (serviceDescriptor is not null)
        {
            services.Remove(serviceDescriptor);
        }
    }

    public static void EnsureDbCreated<T>(this IServiceCollection services) where T : DbContext
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<T>();
        context.Database.EnsureCreated();
    }
}