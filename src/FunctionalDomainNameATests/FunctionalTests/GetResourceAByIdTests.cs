using ComponentName.FunctionalDomainNameA.Core.ResourceA;
using ComponentName.FunctionalDomainNameA.Core.ResourceA.Ports;
using ComponentName.TestsHelpers;

namespace ComponentName.FunctionalDomainNameATests.FunctionalTests;

public class GetResourceAByIdTests(FunctionalTestsApplicationFactory factory)
    : BaseFunctionalTestsCollectionFixture(factory)
{
    private const string SeedId = "70ed13b5-ff0f-4cfd-9e45-cce72be23a8c";
    private const string ResponseExpectedJson = $$"""
        {
            "someProperty":"fakecontent", "id": "{{SeedId}}"
        }
        """;

    private ResourceAEntity ResponseExpected =>
        DeSerializeData<ResourceAEntity>(ResponseExpectedJson);

    [Fact]
    public async Task GetResourceAById_ResourceAExists_ReturnResourceA()
    {
        // Arrange
        var seed = new ResourceAEntity(new ResourceAId(Guid.Parse(SeedId)), "fakecontent");

        SeedData<IResourceARepository, ResourceAEntity, ResourceAId>(seed);

        // Act
        using var response = await Client.GetAsync(
            $"api/v1/FunctionalDomainNameALowerCase/{SeedId}",
            TestContext.Current.CancellationToken
        );
        Assert.True(response.IsSuccessStatusCode);

        // Assert
        var jsonResponse = await response.Content.ReadAsStringAsync(
            TestContext.Current.CancellationToken
        );
        var result = DeSerializeData<ResourceAEntity>(jsonResponse);

        Assert.Equivalent(ResponseExpected, result);
    }
}
