using Asp.Versioning.Conventions;

using FunctionalDomainNameB.Core.ResourceB.Ports;
using FunctionalDomainNameB.Features.CreateResourceB;
using FunctionalDomainNameB.Features.GetResourceBById;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FunctionalDomainNameB;

public static class FunctionalDomainNameBRegistration
{
    public static WebApplicationBuilder AddFunctionalDomainNameB(this WebApplicationBuilder builder)
    {
        builder.ConfigureApiJsonSerialization();

        builder.Services.AddScoped<ICreateResourceBUseCase, CreateResourceBUseCase>();
        builder.Services.AddScoped<IGetResourceBByIdUseCase, GetResourceBByIdUseCase>();

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddSingleton<IResourceBRepository, ResourceBRepositoryInMemory>();
        }

        return builder;
    }

    public static RouteGroupBuilder MapFunctionalDomainNameBEndpoints(this RouteGroupBuilder group)
    {
        var apiVersionSet = group.NewApiVersionSet()
            .HasApiVersions(ApiVersions.All)
            .ReportApiVersions()
            .Build();


        group.MapGroup("v{v:apiVersion}/FunctionalDomainNameBLowerCase")
            .WithTags("FunctionalDomainNameBLowerCase")
            .WithApiVersionSet(apiVersionSet)
            .MapCreateResourceBEndpoint()
            .MapGetResourceBByIdEndpoint();

        return group;
    }

}
