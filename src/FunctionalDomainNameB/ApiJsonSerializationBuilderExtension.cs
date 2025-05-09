using ComponentName.FunctionalDomainNameB.Core.ResourceB;
using ComponentName.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;

namespace ComponentName.FunctionalDomainNameB;

internal static class ApiJsonSerializationBuilderExtension
{
    public static WebApplicationBuilder ConfigureApiJsonSerialization(
        this WebApplicationBuilder builder
    )
    {
        builder.Services.Configure<JsonOptions>(options =>
            options.SerializerOptions.Converters.Add(new EntityIdConverter<ResourceBId>())
        );
        return builder;
    }
}
