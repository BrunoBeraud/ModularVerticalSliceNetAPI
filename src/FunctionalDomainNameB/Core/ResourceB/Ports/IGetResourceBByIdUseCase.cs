using SharedKernel;

namespace FunctionalDomainNameB.Core.ResourceB.Ports;

internal interface IGetResourceBByIdUseCase
{

    Result<GetResourceBByIdResponse, GetResourceBByIdNotFoundError> GetResourceBById(GetResourceBByIdRequest request);
}

internal sealed record GetResourceBByIdRequest(ResourceBId ResourceRequestedId);
internal sealed record GetResourceBByIdResponse(ResourceBEntity ResourceBRequested);
internal sealed record GetResourceBByIdNotFoundError(ResourceBId IdRequestedNotFound);


