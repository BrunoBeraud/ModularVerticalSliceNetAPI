using Asp.Versioning.Conventions;

using FluentValidation;

using FunctionalDomainNameB.Core.ResourceB.Ports;
using FunctionalDomainNameB.Features.CreateResourceB;
using FunctionalDomainNameB.Features.GetResourceBById;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

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

        builder.Services.AddValidatorsFromAssemblyContaining(typeof(FunctionalDomainNameBRegistration), includeInternalTypes: true);
        builder.Services.AddFluentValidationAutoValidation();

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
            .AddFluentValidationAutoValidation()
            .MapCreateResourceBEndpoint()
            .MapGetResourceBByIdEndpoint();

        return group;
    }

}
