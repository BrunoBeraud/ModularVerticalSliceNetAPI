using FunctionalDomainNameA.Core.ResourceA;

using SharedKernel;

namespace FunctionalDomainNameA.Core.ResourceA.Ports;

internal interface IGetResourceAByIdUseCase
{

    Result<GetResourceAByIdResponse, GetResourceAByIdNotFoundError> GetResourceAById(GetResourceAByIdRequest request);
}

internal sealed record GetResourceAByIdRequest(ResourceAId ResourceRequestedId);
internal sealed record GetResourceAByIdResponse(ResourceAEntity ResourceARequested);
internal sealed record GetResourceAByIdNotFoundError(ResourceAId IdRequestedNotFound);

