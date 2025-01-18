using AutoFixture.Xunit2;

using FunctionalDomainNameA.Core.ResourceA.Ports;
using FunctionalDomainNameA.Features.GetResourceAById;

using Moq;

using SharedKernel;

using TestsHelpers;

namespace FunctionalDomainNameATests.UseCasesTests;

public class GetResourceAByIdUseCaseTests
{
    [Theory, AutoDomainData]
    internal void GetResourceAById_ResourceANotExists_ReturnsGetResourceAByIdNotFoundError(
        [Frozen] Mock<IResourceARepository> resourceARepositoryMock,
        GetResourceAByIdUseCase sut,
        GetResourceAByIdRequest request)
    {
        // Arrange
        resourceARepositoryMock
            .Setup(x => x.GetById(request.ResourceRequestedId))
            .Returns(value: null);

        Result<GetResourceAByIdResponse, GetResourceAByIdNotFoundError> expected = new GetResourceAByIdNotFoundError(IdRequestedNotFound: request.ResourceRequestedId);

        // Act
        var result = sut.GetResourceAById(request);

        // Assert
        Assert.Equivalent(result, expected);
    }
}