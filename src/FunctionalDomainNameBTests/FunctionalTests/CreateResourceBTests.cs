using System.Net.Http.Json;

using FluentAssertions;

using FunctionalDomainNameB.Core.ResourceB.Ports;
using FunctionalDomainNameB.Core.ResourceB;

using TestsHelpers;

namespace FunctionalDomainNameBTests.FunctionalTests;

public class CreateResourceBTests
{
    public class GetResourceBByIdTests(FunctionalTestsApplicationFactory factory)
    : BaseFunctionalTestsCollectionFixture(factory)
    {
        private const string _requestPayLoadJson = "{\"someProperty\":\"fakecontent\"}";

        [Fact]
        public async Task CreateResourceB_CreateResourceBRequestValid_ReturnResourceB()
        {
            // Arrange
            var _requestPayLoad = DeSerializeData<FunctionalDomainNameB.Features.CreateResourceB.CreateResourceBRequest>(_requestPayLoadJson);

            // Act
            using var response = await Client.PostAsJsonAsync(
                requestUri: $"api/v1/FunctionalDomainNameBLowerCase",
                value: _requestPayLoad);

            var expected = new ResourceBEntity(_requestPayLoad.SomeProperty);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var expectedResponse = DeSerializeData<ResourceBEntity>(jsonResponse);

            expectedResponse.Should().BeEquivalentTo(expected, options => options.Excluding(x => x.Id));
            expectedResponse.Id.Should().NotBeNull();

            var expectedValueInserted = GetDataById<IResourceBRepository, ResourceBEntity, ResourceBId>(expectedResponse.Id);
            expectedValueInserted.Should().BeEquivalentTo(expected);
        }
    }

}
