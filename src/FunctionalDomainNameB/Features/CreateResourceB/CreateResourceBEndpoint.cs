using System.Net;
using ComponentName.FunctionalDomainNameB.Core.ResourceB.Ports;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ComponentName.FunctionalDomainNameB.Features.CreateResourceB;

internal static class CreateResourceBEndpoint
{
    public static RouteGroupBuilder MapCreateResourceBEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", CreateResourceB).MapToApiVersion(ApiVersions.V1);

        return group;
    }

    private static IResult CreateResourceB(
        [FromBody] CreateResourceBRequest request,
        [FromServices] ICreateResourceBUseCase useCase
    )
    {
        var result = useCase.CreateResourceB(new(request.SomeProperty));

        return result.Match<IResult>(
            success: (successResult) =>
                TypedResults.Created(
                    uri: $"/FunctionalDomainNameBLowerCase/{successResult.ResourceBCreated.Id}",
                    value: new CreateResourceBResponse(
                        new Guid(successResult.ResourceBCreated.Id),
                        successResult.ResourceBCreated.SomeProperty
                    )
                ),
            failure: (errorResult) =>
                result.Error switch
                {
                    CreateResourceBError => TypedResults.BadRequest(
                        new ProblemDetails()
                        {
                            Status = (int)HttpStatusCode.BadRequest,
                            Title = "An error occurred",
                            Type = nameof(CreateResourceBError),
                            Detail = result.Error.ErrorMessage,
                        }
                    ),
                    _ => TypedResults.UnprocessableEntity(),
                }
        );
    }
}
