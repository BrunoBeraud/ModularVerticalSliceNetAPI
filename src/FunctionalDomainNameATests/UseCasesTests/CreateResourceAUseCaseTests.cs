using AutoFixture;

using FluentAssertions;

using FunctionalDomainNameA.Core.ResourceA.Ports;
using FunctionalDomainNameA.Features.CreateResourceA;

using SharedKernel;

using TestsHelpers;

namespace FunctionalDomainNameATests.UseCasesTests;

public class CreateResourceAUseCaseTests
{
    [Theory, AutoDomainData]
    internal void CreateResourceA_WrongSomeProperty_ReturnsCreateResourceAError(
        CreateResourceAUseCase sut,
        Fixture fixture)
    {
        var request = fixture
            .Build<FunctionalDomainNameA.Core.ResourceA.Ports.CreateResourceARequest>()
            .With(x => x.SomeProperty, "raiseError")
            .Create();
        Result<CreateResourceAResponse, CreateResourceAError> expected = new CreateResourceAError(ErrorMessage: "SomeErrorRaised");

        // Act
        var result = sut.CreateResourceA(request);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}