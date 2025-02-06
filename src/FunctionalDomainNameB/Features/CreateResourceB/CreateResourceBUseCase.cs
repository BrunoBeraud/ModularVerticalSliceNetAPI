using ComponentName.FunctionalDomainNameB.Core.ResourceB;
using ComponentName.FunctionalDomainNameB.Core.ResourceB.Ports;
using ComponentName.SharedKernel;

namespace ComponentName.FunctionalDomainNameB.Features.CreateResourceB;

internal class CreateResourceBUseCase(IResourceBRepository resourceBRepository)
    : ICreateResourceBUseCase
{
    public Result<CreatedResourceBResponse, CreateResourceBError> CreateResourceB(
        Core.ResourceB.Ports.CreateResourceBRequest request
    )
    {
        if (request.SomeProperty == "raiseError")
        {
            return new CreateResourceBError(ErrorMessage: "SomeErrorRaised");
        }

        var entityToAdd = new ResourceBEntity(request.SomeProperty);

        resourceBRepository.Create(entityToAdd);

        return new CreatedResourceBResponse(ResourceBCreated: entityToAdd);
    }
}
