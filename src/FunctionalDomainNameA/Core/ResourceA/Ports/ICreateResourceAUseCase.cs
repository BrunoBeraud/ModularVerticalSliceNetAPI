using SharedKernel;

namespace FunctionalDomainNameA.Core.ResourceA.Ports;

internal interface ICreateResourceAUseCase
{
    Result<CreateResourceAResponse, CreateResourceAError> CreateResourceA(CreateResourceARequest request);
}

internal sealed record CreateResourceARequest(string SomeProperty);
internal sealed record CreateResourceAResponse(ResourceAEntity ResourceACreated);
internal sealed record CreateResourceAError(string ErrorMessage);
