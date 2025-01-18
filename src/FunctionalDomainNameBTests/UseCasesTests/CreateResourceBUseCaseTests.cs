using AutoFixture;

using FunctionalDomainNameB.Core.ResourceB.Ports;
using FunctionalDomainNameB.Features.CreateResourceB;

using SharedKernel;

using TestsHelpers;

namespace FunctionalDomainNameBTests.UseCasesTests;

public class CreateResourceBUseCaseTests
{
    [Theory, AutoDomainData]
    internal void CreateResourceB_WrongSomeProperty_ReturnsCreateResourceBError(
        CreateResourceBUseCase sut,
        Fixture fixture)
    {
        var request = fixture
            .Build<FunctionalDomainNameB.Core.ResourceB.Ports.CreateResourceBRequest>()
            .With(x => x.SomeProperty, "raiseError")
            .Create();
        Result<CreateResourceBResponse, CreateResourceBError> expected = new CreateResourceBError(ErrorMessage: "SomeErrorRaised");

        // Act
        var result = sut.CreateResourceB(request);

        // Assert
        Assert.Equivalent(result, expected);
    }
}