using System.Net;
using ComponentName.FunctionalDomainNameA.Core.ResourceA.Ports;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ComponentName.FunctionalDomainNameA.Features.CreateResourceA;

internal static class CreateResourceAEndpoint
{
    public static RouteGroupBuilder MapCreateResourceAEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", CreateResourceA).MapToApiVersion(ApiVersions.V1);

        return group;
    }

    private static IResult CreateResourceA(
        [FromBody] CreateResourceARequest request,
        [FromServices] ICreateResourceAUseCase useCase
    )
    {
        var result = useCase.CreateResourceA(new(request.SomeProperty));

        return result.Match<IResult>(
            success: (successResult) =>
                TypedResults.Created(
                    uri: $"/FunctionalDomainNameALowerCase/{successResult.ResourceACreated.Id}",
                    value: new CreateResourceAResponse(
                        new Guid(successResult.ResourceACreated.Id),
                        successResult.ResourceACreated.SomeProperty
                    )
                ),
            failure: (errorResult) =>
                result.Error switch
                {
                    CreateResourceAError => TypedResults.BadRequest(
                        new ProblemDetails()
                        {
                            Status = (int)HttpStatusCode.BadRequest,
                            Title = "An error occurred",
                            Type = nameof(CreateResourceAError),
                            Detail = result.Error.ErrorMessage,
                        }
                    ),
                    _ => TypedResults.UnprocessableEntity(),
                }
        );
    }
}
