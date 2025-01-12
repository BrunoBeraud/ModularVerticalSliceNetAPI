using FunctionalDomainNameB.Core.ResourceB.Ports;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FunctionalDomainNameB.Features.GetResourceBById;

internal static class GetResourceBByIdEndpoint
{

    public static RouteGroupBuilder MapGetResourceBByIdEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{ResourceBId:guid}", GetResourceBbyId)
            .MapToApiVersion(ApiVersions.V1);

        return group;
    }

    private static IResult GetResourceBbyId(
         Guid ResourceBId,
        [FromServices] IGetResourceBByIdUseCase _useCase)
    {
        var result = _useCase.GetResourceBById(new(ResourceBId));

        return result.Match<IResult>(
        success: (successResult) => TypedResults.Ok(successResult.ResourceBRequested),
        failure: (errorResult) => result.Error switch
        {
            GetResourceBByIdNotFoundError => TypedResults.NotFound($"ResourceB {ResourceBId} not found"),
            _ => TypedResults.UnprocessableEntity()
        });
    }
}


