using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ComponentName.Host;

internal static class ObservabilityBuilderExtension
{
    public static WebApplicationBuilder ConfigureObservability(this WebApplicationBuilder builder)
    {
        // Export logs via OLTP
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        var otel = builder.Services.AddOpenTelemetry();

        var otlpServiceName = builder.Configuration["OTELSERVICE_NAME"];
        ArgumentNullException.ThrowIfNull(otlpServiceName);
        otel.ConfigureResource(builder => builder.AddService(serviceName: otlpServiceName));

        // Add Metrics for ASP.NET Core and export via OTLP
        otel.WithMetrics(metrics =>
            // Metrics provider from OpenTelemetry
            metrics.AddAspNetCoreInstrumentation()
        );

        // Add Tracing for ASP.NET Core and export via OTLP
        otel.WithTracing(tracing => tracing.AddAspNetCoreInstrumentation());

        // Export OpenTelemetry data via OTLP, using env vars for the configuration
        var otlpEndpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];
        ArgumentNullException.ThrowIfNull(otlpEndpoint);

        otel.UseOtlpExporter();

        return builder;
    }
}
