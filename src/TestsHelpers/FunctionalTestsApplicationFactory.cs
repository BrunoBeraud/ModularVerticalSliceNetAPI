using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace ComponentName.TestsHelpers;

public class FunctionalTestsApplicationFactory : WebApplicationFactory<Program>
{
    // build Up component dependencies (db, message broker, etc.) with TestContainers here (implement IAsyncLifetime interface)

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Override application configuration
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(
                [new("ConfigurationKeyToOverrideSample", "ConfigurationValueToOverrideSample")]
            )
            .Build();

        builder.UseConfiguration(config);

        builder.ConfigureServices(services =>
        {
            // Override / Configure application services
        });

        builder.UseEnvironment("Development");
    }
}
