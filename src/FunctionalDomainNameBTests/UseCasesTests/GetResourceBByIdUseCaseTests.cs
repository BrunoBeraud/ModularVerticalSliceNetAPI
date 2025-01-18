using AutoFixture.Xunit2;

using FunctionalDomainNameB.Core.ResourceB.Ports;
using FunctionalDomainNameB.Features.GetResourceBById;

using Moq;

using SharedKernel;

using TestsHelpers;

namespace FunctionalDomainNameBTests.UseCasesTests;

public class GetResourceBByIdUseCaseTests
{
    [Theory, AutoDomainData]
    internal void GetResourceBById_ResourceBNotExists_ReturnsGetResourceBByIdNotFoundError(
        [Frozen] Mock<IResourceBRepository> ResourceBRepositoryMock,
        GetResourceBByIdUseCase sut,
        GetResourceBByIdRequest request)
    {
        // Arrange
        ResourceBRepositoryMock
            .Setup(x => x.GetById(request.ResourceRequestedId))
            .Returns(value: null);

        Result<GetResourceBByIdResponse, GetResourceBByIdNotFoundError> expected = new GetResourceBByIdNotFoundError(IdRequestedNotFound: request.ResourceRequestedId);

        // Act
        var result = sut.GetResourceBById(request);

        // Assert
        Assert.Equivalent(result, expected);
    }
}