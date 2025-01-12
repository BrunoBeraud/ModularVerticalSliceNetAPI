using FunctionalDomainNameB.Core.ResourceB.Ports;
using FunctionalDomainNameB.Core.ResourceB;

using SharedKernel;

namespace FunctionalDomainNameB.Features.CreateResourceB;

internal class CreateResourceBUseCase(IResourceBRepository _ResourceBRepository) : ICreateResourceBUseCase
{
    public Result<CreateResourceBResponse, CreateResourceBError> CreateResourceB(Core.ResourceB.Ports.CreateResourceBRequest request)
    {
        if (request.SomeProperty == "raiseError")
        {
            return new CreateResourceBError(ErrorMessage: "SomeErrorRaised");
        }

        var entityToAdd = new ResourceBEntity(request.SomeProperty);

        _ResourceBRepository.Create(entityToAdd);

        return new CreateResourceBResponse(ResourceBCreated: entityToAdd);
    }
}
