using FunctionalDomainNameA.Core.ResourceA.Ports;
using FunctionalDomainNameA.Core.ResourceA;

using SharedKernel;

namespace FunctionalDomainNameA.Features.CreateResourceA;

internal class CreateResourceAUseCase(IResourceARepository _resourceARepository) : ICreateResourceAUseCase
{
    public Result<CreateResourceAResponse, CreateResourceAError> CreateResourceA(Core.ResourceA.Ports.CreateResourceARequest request)
    {
        if (request.SomeProperty == "raiseError")
        {
            return new CreateResourceAError(ErrorMessage: "SomeErrorRaised");
        }

        var entityToAdd = new ResourceAEntity(request.SomeProperty);

        _resourceARepository.Create(entityToAdd);

        return new CreateResourceAResponse(ResourceACreated: entityToAdd);
    }
}
