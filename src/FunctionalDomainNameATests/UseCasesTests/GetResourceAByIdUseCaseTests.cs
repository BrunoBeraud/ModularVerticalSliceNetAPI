using AutoFixture.Xunit3;
using ComponentName.FunctionalDomainNameA.Core.ResourceA.Ports;
using ComponentName.FunctionalDomainNameA.Features.GetResourceAById;
using ComponentName.SharedKernel;
using ComponentName.TestsHelpers;
using Moq;

namespace ComponentName.FunctionalDomainNameATests.UseCasesTests;

public class GetResourceAByIdUseCaseTests
{
    [Theory, AutoDomainData]
    internal void GetResourceAById_ResourceANotExists_ReturnsGetResourceAByIdNotFoundError(
        [Frozen] Mock<IResourceARepository> resourceARepositoryMock,
        GetResourceAByIdUseCase sut,
        GetResourceAByIdRequest request
    )
    {
        // Arrange
        resourceARepositoryMock
            .Setup(x => x.GetById(request.ResourceRequestedId))
            .Returns(value: null);

        Result<GetResourceAByIdResponse, GetResourceAByIdNotFoundError> expected =
            new GetResourceAByIdNotFoundError(IdRequestedNotFound: request.ResourceRequestedId);

        // Act
        var result = sut.GetResourceAById(request);

        // Assert
        Assert.Equivalent(result, expected);
    }
}
