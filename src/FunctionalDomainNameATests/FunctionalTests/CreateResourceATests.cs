using System.Net.Http.Json;

using FluentAssertions;

using FunctionalDomainNameA.Core.ResourceA.Ports;
using FunctionalDomainNameA.Core.ResourceA;

using TestsHelpers;

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
            response.IsSuccessStatusCode.Should().BeTrue();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var expectedResponse = DeSerializeData<ResourceAEntity>(jsonResponse);

            expectedResponse.Should().BeEquivalentTo(expected, options => options.Excluding(x => x.Id));
            expectedResponse.Id.Should().NotBeNull();

            var expectedValueInserted = GetDataById<IResourceARepository, ResourceAEntity, ResourceAId>(expectedResponse.Id);
            expectedValueInserted.Should().BeEquivalentTo(expected);
        }
    }

}
