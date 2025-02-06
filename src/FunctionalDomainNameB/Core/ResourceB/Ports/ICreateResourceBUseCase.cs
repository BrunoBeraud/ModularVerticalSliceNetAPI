using ComponentName.SharedKernel;

namespace ComponentName.FunctionalDomainNameB.Core.ResourceB.Ports;

internal interface ICreateResourceBUseCase
{
    Result<CreatedResourceBResponse, CreateResourceBError> CreateResourceB(
        CreateResourceBRequest request
    );
}

internal sealed record CreateResourceBRequest(string SomeProperty);

internal sealed record CreatedResourceBResponse(ResourceBEntity ResourceBCreated);

internal sealed record CreateResourceBError(string ErrorMessage);
