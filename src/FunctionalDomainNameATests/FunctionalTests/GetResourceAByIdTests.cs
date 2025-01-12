using FluentAssertions;

using FunctionalDomainNameA.Core.ResourceA.Ports;
using FunctionalDomainNameA.Core.ResourceA;

using TestsHelpers;

namespace FunctionalDomainNameATests.FunctionalTests;

public class GetResourceAByIdTests(FunctionalTestsApplicationFactory factory)
    : BaseFunctionalTestsCollectionFixture(factory)
{
    [Fact]
    public async Task GetResourceAById_ResourceAExists_ReturnResourceA()
    {
        // Arrange
        var seed = new ResourceAEntity(
            id: Guid.Parse("6e37bd9c-536f-4797-916b-3fb751d55c10"),
            someProperty: "test data");

        SeedData<IResourceARepository, ResourceAEntity, ResourceAId>(seed);

        // Act
        using var response = await Client.GetAsync($"api/v1/FunctionalDomainNameALowerCase/{seed.Id}");
        response.IsSuccessStatusCode.Should().BeTrue();

        // Assert
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = DeSerializeData<ResourceAEntity>(jsonResponse);

        result.Should().BeEquivalentTo(seed);
    }
}
