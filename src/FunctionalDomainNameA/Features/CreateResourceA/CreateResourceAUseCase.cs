using ComponentName.FunctionalDomainNameA.Core.ResourceA;
using ComponentName.FunctionalDomainNameA.Core.ResourceA.Ports;
using ComponentName.SharedKernel;

namespace ComponentName.FunctionalDomainNameA.Features.CreateResourceA;

internal class CreateResourceAUseCase(IResourceARepository resourceARepository)
    : ICreateResourceAUseCase
{
    public Result<CreatedResourceAResponse, CreateResourceAError> CreateResourceA(
        Core.ResourceA.Ports.CreateResourceARequest request
    )
    {
        if (request.SomeProperty == "raiseError")
        {
            return new CreateResourceAError(ErrorMessage: "SomeErrorRaised");
        }

        var entityToAdd = new ResourceAEntity(request.SomeProperty);

        resourceARepository.Create(entityToAdd);

        return new CreatedResourceAResponse(ResourceACreated: entityToAdd);
    }
}
