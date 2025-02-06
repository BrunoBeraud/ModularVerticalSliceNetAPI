using ComponentName.FunctionalDomainNameB.Core.ResourceB.Ports;
using ComponentName.SharedKernel;

namespace ComponentName.FunctionalDomainNameB.Features.GetResourceBById;

internal class GetResourceBByIdUseCase(IResourceBRepository resourceBRepository)
    : IGetResourceBByIdUseCase
{
    public Result<GetResourceBByIdResponse, GetResourceBByIdNotFoundError> GetResourceBById(
        GetResourceBByIdRequest request
    )
    {
        var ResourceB = resourceBRepository.GetById(request.ResourceRequestedId);

        return ResourceB is null
            ? new GetResourceBByIdNotFoundError(IdRequestedNotFound: request.ResourceRequestedId)
            : new GetResourceBByIdResponse(ResourceB);
    }
}
