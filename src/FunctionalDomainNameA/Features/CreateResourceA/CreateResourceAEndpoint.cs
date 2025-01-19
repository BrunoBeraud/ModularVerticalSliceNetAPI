using System.Net;

using FunctionalDomainNameA.Core.ResourceA.Ports;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FunctionalDomainNameA.Features.CreateResourceA;

internal static class CreateResourceAEndpoint
{
    public static RouteGroupBuilder MapCreateResourceAEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", CreateResourceA)
            .MapToApiVersion(ApiVersions.V1);

        return group;
    }

    private static IResult CreateResourceA(
        [FromBody] CreateResourceARequest request,
        [FromServices] ICreateResourceAUseCase _useCase)
    {
        var result = _useCase.CreateResourceA(new(request.SomeProperty));

        return result.Match<IResult>(
            success: (successResult) =>
                TypedResults.Created(
                    uri: $"/FunctionalDomainNameALowerCase/{successResult.ResourceACreated.Id}",
                    value: successResult.ResourceACreated)
            ,
            failure: (errorResult) => result.Error switch
            {
                CreateResourceAError => TypedResults.BadRequest(new ProblemDetails()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Title = "An error occurred",
                    Type = nameof(CreateResourceAError),
                    Detail = result.Error.ErrorMessage
                }),
                _ => TypedResults.UnprocessableEntity()
            });
    }
}