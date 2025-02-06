using ComponentName.FunctionalDomainNameA.Core.ResourceA;
using ComponentName.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;

namespace ComponentName.FunctionalDomainNameA;

internal static class ApiJsonSerializationBuilderExtension
{
    public static WebApplicationBuilder ConfigureApiJsonSerialization(
        this WebApplicationBuilder builder
    )
    {
        builder.Services.Configure<JsonOptions>(options =>
            options.SerializerOptions.Converters.Add(new EntityIdConverter<ResourceAId>())
        );
        return builder;
    }
}
