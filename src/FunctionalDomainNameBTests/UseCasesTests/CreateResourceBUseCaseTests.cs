using AutoFixture;
using ComponentName.FunctionalDomainNameB.Core.ResourceB.Ports;
using ComponentName.FunctionalDomainNameB.Features.CreateResourceB;
using ComponentName.SharedKernel;
using ComponentName.TestsHelpers;

namespace ComponentName.FunctionalDomainNameBTests.UseCasesTests;

public class CreateResourceBUseCaseTests
{
    [Theory, AutoDomainData]
    internal void CreateResourceB_WrongSomeProperty_ReturnsCreateResourceBError(
        CreateResourceBUseCase sut,
        Fixture fixture
    )
    {
        var request = fixture
            .Build<FunctionalDomainNameB.Core.ResourceB.Ports.CreateResourceBRequest>()
            .With(x => x.SomeProperty, "raiseError")
            .Create();
        Result<CreatedResourceBResponse, CreateResourceBError> expected = new CreateResourceBError(
            ErrorMessage: "SomeErrorRaised"
        );

        // Act
        var result = sut.CreateResourceB(request);

        // Assert
        Assert.Equivalent(result, expected);
    }
}
