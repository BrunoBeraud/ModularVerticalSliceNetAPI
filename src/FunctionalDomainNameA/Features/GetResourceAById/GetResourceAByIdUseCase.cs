using FunctionalDomainNameA.Core.ResourceA.Ports;

using SharedKernel;

namespace FunctionalDomainNameA.Features.GetResourceAById;

internal class GetResourceAByIdUseCase(IResourceARepository resourceARepository) : IGetResourceAByIdUseCase
{
    public Result<GetResourceAByIdResponse, GetResourceAByIdNotFoundError> GetResourceAById(GetResourceAByIdRequest request)
    {
        var resourceA = resourceARepository.GetById(request.ResourceRequestedId);

        return resourceA is null
            ? new GetResourceAByIdNotFoundError(IdRequestedNotFound: request.ResourceRequestedId)
            : new GetResourceAByIdResponse(resourceA);
    }
}
