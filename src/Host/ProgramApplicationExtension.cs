using ComponentName.FunctionalDomainNameA;
using ComponentName.FunctionalDomainNameB;

namespace ComponentName.Host;

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

        app.UseMiddleware<LogFailedIncomingRequestMiddleware>();

        app.UseExceptionHandler(
            new ExceptionHandlerOptions
            {
                StatusCodeSelector = ex => StatusCodes.Status500InternalServerError,
            }
        );

        return app;
    }
}
