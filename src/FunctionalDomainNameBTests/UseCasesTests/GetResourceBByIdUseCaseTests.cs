using AutoFixture.Xunit3;
using ComponentName.FunctionalDomainNameB.Core.ResourceB.Ports;
using ComponentName.FunctionalDomainNameB.Features.GetResourceBById;
using ComponentName.SharedKernel;
using ComponentName.TestsHelpers;
using Moq;

namespace ComponentName.FunctionalDomainNameBTests.UseCasesTests;

public class GetResourceBByIdUseCaseTests
{
    [Theory, AutoDomainData]
    internal void GetResourceBById_ResourceBNotExists_ReturnsGetResourceBByIdNotFoundError(
        [Frozen] Mock<IResourceBRepository> ResourceBRepositoryMock,
        GetResourceBByIdUseCase sut,
        GetResourceBByIdRequest request
    )
    {
        // Arrange
        ResourceBRepositoryMock
            .Setup(x => x.GetById(request.ResourceRequestedId))
            .Returns(value: null);

        Result<GetResourceBByIdResponse, GetResourceBByIdNotFoundError> expected =
            new GetResourceBByIdNotFoundError(IdRequestedNotFound: request.ResourceRequestedId);

        // Act
        var result = sut.GetResourceBById(request);

        // Assert
        Assert.Equivalent(result, expected);
    }
}
