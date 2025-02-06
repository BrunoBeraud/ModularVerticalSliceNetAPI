using ComponentName.FunctionalDomainNameB.Core.ResourceB;
using ComponentName.FunctionalDomainNameB.Core.ResourceB.Ports;
using ComponentName.TestsHelpers;

namespace ComponentName.FunctionalDomainNameATests.FunctionalTests;

public class GetResourceBByIdTests(FunctionalTestsApplicationFactory factory)
    : BaseFunctionalTestsCollectionFixture(factory)
{
    private const string SeedId = "70ed13b5-ff0f-4cfd-9e45-cce72be23a8c";
    private const string ResponseExpectedJson = $$"""
        {
            "someProperty":"fakecontent", "id": "{{SeedId}}"
        }
        """;

    private ResourceBEntity ResponseExpected =>
        DeSerializeData<ResourceBEntity>(ResponseExpectedJson);

    [Fact]
    public async Task GetResourceBById_ResourceBExists_ReturnResourceB()
    {
        // Arrange
        var seed = new ResourceBEntity(new ResourceBId(Guid.Parse(SeedId)), "fakecontent");

        SeedData<IResourceBRepository, ResourceBEntity, ResourceBId>(seed);

        // Act
        using var response = await Client.GetAsync(
            $"api/v1/FunctionalDomainNameBLowerCase/{SeedId}",
            TestContext.Current.CancellationToken
        );
        Assert.True(response.IsSuccessStatusCode);

        // Assert
        var jsonResponse = await response.Content.ReadAsStringAsync(
            TestContext.Current.CancellationToken
        );
        var result = DeSerializeData<ResourceBEntity>(jsonResponse);

        Assert.Equivalent(ResponseExpected, result);
    }
}
