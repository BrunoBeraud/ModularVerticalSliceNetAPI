using FunctionalDomainNameA.Core.ResourceA.Ports;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FunctionalDomainNameA.Features.GetResourceAById;

internal static class GetResourceAByIdEndpoint
{

    public static RouteGroupBuilder MapGetResourceAByIdEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{resourceAId:guid}", GetResourceAbyId)
            .MapToApiVersion(ApiVersions.V1);

        return group;
    }

    private static IResult GetResourceAbyId(
         Guid resourceAId,
        [FromServices] IGetResourceAByIdUseCase _useCase)
    {
        var result = _useCase.GetResourceAById(new(resourceAId));

        return result.Match<IResult>(
        success: (successResult) => TypedResults.Ok(successResult.ResourceARequested),
        failure: (errorResult) => result.Error switch
        {
            GetResourceAByIdNotFoundError => TypedResults.NotFound($"ResourceA {resourceAId} not found"),
            _ => TypedResults.UnprocessableEntity()
        });
    }
}


