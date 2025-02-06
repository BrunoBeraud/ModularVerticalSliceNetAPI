using AutoFixture;
using ComponentName.FunctionalDomainNameA.Core.ResourceA.Ports;
using ComponentName.FunctionalDomainNameA.Features.CreateResourceA;
using ComponentName.SharedKernel;
using ComponentName.TestsHelpers;

namespace ComponentName.FunctionalDomainNameATests.UseCasesTests;

public class CreateResourceAUseCaseTests
{
    [Theory, AutoDomainData]
    internal void CreateResourceA_WrongSomeProperty_ReturnsCreateResourceAError(
        CreateResourceAUseCase sut,
        Fixture fixture
    )
    {
        var request = fixture
            .Build<FunctionalDomainNameA.Core.ResourceA.Ports.CreateResourceARequest>()
            .With(x => x.SomeProperty, "raiseError")
            .Create();
        Result<CreatedResourceAResponse, CreateResourceAError> expected = new CreateResourceAError(
            ErrorMessage: "SomeErrorRaised"
        );

        // Act
        var result = sut.CreateResourceA(request);

        // Assert
        Assert.Equivalent(result, expected);
    }
}
