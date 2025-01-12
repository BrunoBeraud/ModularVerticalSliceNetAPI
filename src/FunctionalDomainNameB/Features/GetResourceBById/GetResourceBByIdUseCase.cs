using FunctionalDomainNameB.Core.ResourceB.Ports;

using SharedKernel;

namespace FunctionalDomainNameB.Features.GetResourceBById;

internal class GetResourceBByIdUseCase(IResourceBRepository ResourceBRepository) : IGetResourceBByIdUseCase
{
    public Result<GetResourceBByIdResponse, GetResourceBByIdNotFoundError> GetResourceBById(GetResourceBByIdRequest request)
    {
        var ResourceB = ResourceBRepository.GetById(request.ResourceRequestedId);

        return ResourceB is null
            ? new GetResourceBByIdNotFoundError(IdRequestedNotFound: request.ResourceRequestedId)
            : new GetResourceBByIdResponse(ResourceB);
    }
}
