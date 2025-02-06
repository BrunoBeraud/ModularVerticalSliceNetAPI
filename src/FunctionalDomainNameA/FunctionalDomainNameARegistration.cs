using Asp.Versioning.Conventions;
using ComponentName.FunctionalDomainNameA.Core.ResourceA.Ports;
using ComponentName.FunctionalDomainNameA.Features.CreateResourceA;
using ComponentName.FunctionalDomainNameA.Features.GetResourceAById;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace ComponentName.FunctionalDomainNameA;

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

        builder.Services.AddValidatorsFromAssemblyContaining(
            typeof(FunctionalDomainNameARegistration),
            includeInternalTypes: true
        );
        builder.Services.AddFluentValidationAutoValidation();

        return builder;
    }

    public static RouteGroupBuilder MapFunctionalDomainNameAEndpoints(this RouteGroupBuilder group)
    {
        var apiVersionSet = group
            .NewApiVersionSet()
            .HasApiVersions(ApiVersions.All)
            .ReportApiVersions()
            .Build();

        group
            .MapGroup("v{v:apiVersion}/FunctionalDomainNameALowerCase")
            .WithTags("FunctionalDomainNameALowerCase")
            .WithApiVersionSet(apiVersionSet)
            .AddFluentValidationAutoValidation()
            .MapCreateResourceAEndpoint()
            .MapGetResourceAByIdEndpoint();

        return group;
    }
}
