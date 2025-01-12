using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Host;

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

        var OtlpServiceName = builder.Configuration["OpenTelemetry:OTELSERVICE_NAME"];
        ArgumentNullException.ThrowIfNull(OtlpServiceName);
        otel.ConfigureResource(builder => builder.AddService(serviceName: OtlpServiceName));

        // Add Metrics for ASP.NET Core and export via OTLP
        otel.WithMetrics(metrics =>
        {
            // Metrics provider from OpenTelemetry
            metrics.AddAspNetCoreInstrumentation();
        });

        // Add Tracing for ASP.NET Core and export via OTLP
        otel.WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation();
        });

        // Export OpenTelemetry data via OTLP, using env vars for the configuration
        var OtlpEndpoint = builder.Configuration["OpenTelemetry:OTEL_EXPORTER_OTLP_ENDPOINT"];
        ArgumentNullException.ThrowIfNull(OtlpEndpoint);

        otel.UseOtlpExporter();

        return builder;
    }
}

