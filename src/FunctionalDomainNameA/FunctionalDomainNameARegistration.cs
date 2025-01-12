using Asp.Versioning.Conventions;

using FunctionalDomainNameA.Core.ResourceA.Ports;
using FunctionalDomainNameA.Features.CreateResourceA;
using FunctionalDomainNameA.Features.GetResourceAById;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FunctionalDomainNameA;

public static class FunctionalDomainNameARegistration
{
    public static WebApplicationBuilder AddFunctionalDomainNameA(this WebApplicationBuilder builder)
    {
        builder.ConfigureApiJsonSerialization();

        builder.Services.AddScoped<ICreateResourceAUseCase, CreateResourceAUseCase>();
        builder.Services.AddScoped<IGetResourceAByIdUseCase, GetResourceAByIdUseCase>();

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddSingleton<IResourceARepository, ResourceARepositoryInMemory>();
        }

        return builder;
    }

    public static RouteGroupBuilder MapFunctionalDomainNameAEndpoints(this RouteGroupBuilder group)
    {
        var apiVersionSet = group.NewApiVersionSet()
            .HasApiVersions(ApiVersions.All)
            .ReportApiVersions()
            .Build();


        group.MapGroup("v{v:apiVersion}/FunctionalDomainNameALowerCase")
            .WithTags("FunctionalDomainNameALowerCase")
            .WithApiVersionSet(apiVersionSet)
            .MapCreateResourceAEndpoint()
            .MapGetResourceAByIdEndpoint();

        return group;
    }

}
