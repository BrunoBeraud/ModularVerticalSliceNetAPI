using System.Net.Http.Json;

using FunctionalDomainNameB.Core.ResourceB.Ports;
using FunctionalDomainNameB.Core.ResourceB;

using TestsHelpers;

using DeepEqual.Syntax;

namespace FunctionalDomainNameBTests.FunctionalTests;

public class CreateResourceBTests
{
    public class GetResourceBByIdTests(FunctionalTestsApplicationFactory factory)
    : BaseFunctionalTestsCollectionFixture(factory)
    {
        private const string RequestPayLoadJson = "{\"someProperty\":\"fakecontent\"}";

        [Fact]
        public async Task CreateResourceB_CreateResourceBRequestValid_ReturnResourceB()
        {
            // Arrange
            var requestPayLoad = DeSerializeData<FunctionalDomainNameB.Features.CreateResourceB.CreateResourceBRequest>(RequestPayLoadJson);

            // Act
            using var response = await Client.PostAsJsonAsync(
                requestUri: $"api/v1/FunctionalDomainNameBLowerCase",
                value: requestPayLoad);

            var expected = new ResourceBEntity(requestPayLoad.SomeProperty);

            // Assert
            Assert.True(response.IsSuccessStatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var expectedResponse = DeSerializeData<ResourceBEntity>(jsonResponse);

            expectedResponse
                .WithDeepEqual(expected)
                .SkipDefault<ResourceBEntity>()
                .IgnoreSourceProperty(x => x.Id)
                .Assert();
            Assert.NotNull(expectedResponse.Id);

            var expectedValueInserted = GetDataById<IResourceBRepository, ResourceBEntity, ResourceBId>(expectedResponse.Id);
            Assert.Equivalent(expectedValueInserted, expected);
        }
    }

}
