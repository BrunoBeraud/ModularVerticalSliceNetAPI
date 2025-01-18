using System.Net.Http.Json;

using FunctionalDomainNameA.Core.ResourceA.Ports;
using FunctionalDomainNameA.Core.ResourceA;

using TestsHelpers;

using DeepEqual.Syntax;

namespace FunctionalDomainNameATests.FunctionalTests;

public class CreateResourceATests
{
    public class GetResourceAByIdTests(FunctionalTestsApplicationFactory factory)
    : BaseFunctionalTestsCollectionFixture(factory)
    {
        private const string _requestPayLoadJson = "{\"someProperty\":\"fakecontent\"}";

        [Fact]
        public async Task CreateResourceA_CreateResourceARequestValid_ReturnResourceA()
        {
            // Arrange
            var _requestPayLoad = DeSerializeData<FunctionalDomainNameA.Features.CreateResourceA.CreateResourceARequest>(_requestPayLoadJson);

            // Act
            using var response = await Client.PostAsJsonAsync(
                requestUri: $"api/v1/FunctionalDomainNameALowerCase",
                value: _requestPayLoad);

            var expected = new ResourceAEntity(_requestPayLoad.SomeProperty);

            // Assert
            Assert.True(response.IsSuccessStatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var expectedResponse = DeSerializeData<ResourceAEntity>(jsonResponse);

            expectedResponse
                .WithDeepEqual(expected)
                .SkipDefault<ResourceAEntity>()
                .IgnoreSourceProperty(x => x.Id);

            Assert.NotNull(expectedResponse.Id);

            var expectedValueInserted = GetDataById<IResourceARepository, ResourceAEntity, ResourceAId>(expectedResponse.Id);
            Assert.Equivalent(expectedValueInserted, expected);
        }
    }

}
