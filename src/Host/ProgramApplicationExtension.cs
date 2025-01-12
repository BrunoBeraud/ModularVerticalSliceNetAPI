using FunctionalDomainNameA;

using FunctionalDomainNameB;

namespace Host;

public static class ProgramApplicationExtension
{
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app.MapOpenApi();

        // Converts unhandled exceptions into Problem Details responses
        app.UseExceptionHandler();
        // Returns the Problem Details response for (empty) non-successful responses
        app.UseStatusCodePages();

        app.MapGroup("api/")
            .MapFunctionalDomainNameAEndpoints()
            .MapFunctionalDomainNameBEndpoints();

        return app;
    }
}
