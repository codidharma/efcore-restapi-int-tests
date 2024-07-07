
using Testcontainers.MsSql;

namespace WebApi.IntegrationTests.Setup;

public static class MsSqlContainerFactory
{
    public static MsSqlContainer Create()
    {
        var container = new MsSqlBuilder().Build();

        return container;
    }
}
