using SharedKernel;

namespace FunctionalDomainNameB.Core.ResourceB.Ports;

internal interface ICreateResourceBUseCase
{
    Result<CreateResourceBResponse, CreateResourceBError> CreateResourceB(CreateResourceBRequest request);
}

internal sealed record CreateResourceBRequest(string SomeProperty);
internal sealed record CreateResourceBResponse(ResourceBEntity ResourceBCreated);
internal sealed record CreateResourceBError(string ErrorMessage);
