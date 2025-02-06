using ComponentName.SharedKernel;

namespace ComponentName.FunctionalDomainNameA.Core.ResourceA.Ports;

internal interface ICreateResourceAUseCase
{
    Result<CreatedResourceAResponse, CreateResourceAError> CreateResourceA(
        CreateResourceARequest request
    );
}

internal sealed record CreateResourceARequest(string SomeProperty);

internal sealed record CreatedResourceAResponse(ResourceAEntity ResourceACreated);

internal sealed record CreateResourceAError(string ErrorMessage);
