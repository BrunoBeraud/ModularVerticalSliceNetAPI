using FluentAssertions;

using FunctionalDomainNameB.Core.ResourceB.Ports;
using FunctionalDomainNameB.Core.ResourceB;

using TestsHelpers;

namespace FunctionalDomainNameBTests.FunctionalTests;

public class GetResourceBByIdTests(FunctionalTestsApplicationFactory factory)
    : BaseFunctionalTestsCollectionFixture(factory)
{
    [Fact]
    public async Task GetResourceBById_ResourceBExists_ReturnResourceB()
    {
        // Arrange
        var seed = new ResourceBEntity(
            id: Guid.Parse("6e37bd9c-536f-4797-916b-3fb751d55c10"),
            someProperty: "test data");

        SeedData<IResourceBRepository, ResourceBEntity, ResourceBId>(seed);

        // Act
        using var response = await Client.GetAsync($"api/v1/FunctionalDomainNameBLowerCase/{seed.Id}");
        response.IsSuccessStatusCode.Should().BeTrue();

        // Assert
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = DeSerializeData<ResourceBEntity>(jsonResponse);

        result.Should().BeEquivalentTo(seed);
    }
}
