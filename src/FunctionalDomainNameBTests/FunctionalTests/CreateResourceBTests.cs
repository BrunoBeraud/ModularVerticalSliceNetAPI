using System.Net.Mime;
using System.Text;

using ComponentName.FunctionalDomainNameB.Core.ResourceB;
using ComponentName.FunctionalDomainNameB.Core.ResourceB.Ports;
using ComponentName.FunctionalDomainNameB.Features.CreateResourceB;
using ComponentName.TestsHelpers;

namespace ComponentName.FunctionalDomainNameBTests.FunctionalTests;

public class CreateResourceBTests(FunctionalTestsApplicationFactory factory)
    : BaseFunctionalTestsCollectionFixture(factory)
{
    private const string RequestPayLoadJson = """{"someProperty":"fakecontent"}""";
    private const string ResponsePayLoadJson = """{"someProperty":"fakecontent"}""";

    [Fact]
    public async Task CreateResourceB_CreateResourceBRequestValid_ReturnResourceBCreatedResponse()
    {
        // Arrange
        var expectedResponse = DeSerializeData<CreatedResourceBResponse>(ResponsePayLoadJson);

        // Act
        var httpResponse = await SendValidRequest();

        // Assert
        Assert.True(httpResponse.IsSuccessStatusCode);

        var jsonResponse = await httpResponse.Content.ReadAsStringAsync(
            TestContext.Current.CancellationToken
        );
        var response = DeSerializeData<CreatedResourceBResponse>(jsonResponse);

        Assert.Equivalent(expectedResponse, response);
    }

    [Fact]
    public async Task CreateResourceB_CreateResourceBRequestValid_SaveInRepository()
    {
        // Act
        var httpResponse = await SendValidRequest();

        // Assert

        var jsonResponse = await httpResponse.Content.ReadAsStringAsync(
            TestContext.Current.CancellationToken
        );
        var response = DeSerializeData<CreateResourceBResponse>(jsonResponse);

        var expectedValueInserted = GetDataById<IResourceBRepository, ResourceBEntity, ResourceBId>(
            response.Id
        );
        Assert.NotNull(expectedValueInserted);
    }

    private async Task<HttpResponseMessage> SendValidRequest()
    {
        using StringContent jsonContent = new(
            content: RequestPayLoadJson,
            encoding: Encoding.UTF8,
            mediaType: MediaTypeNames.Application.Json
        );

        return await Client.PostAsync(
            requestUri: $"api/v1/FunctionalDomainNameBLowerCase",
            content: jsonContent,
            cancellationToken: TestContext.Current.CancellationToken
        );
    }
}
