using FunctionalDomainNameA;

using FunctionalDomainNameB;

namespace Host;

internal static class ProgramBuilderExtension
{
    public static WebApplicationBuilder ConfigureApplicationBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();
        builder.ConfigureObservability();

        builder.ConfigureApiVersioning();

        // Adds services for using Problem Details format
        builder.Services.AddProblemDetails();

        // Register FunctionalDomains modules services
        builder
            .AddFunctionalDomainNameA()
            .AddFunctionalDomainNameB();

        return builder;
    }
}
