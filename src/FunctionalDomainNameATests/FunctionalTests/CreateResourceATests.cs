using System.Net.Mime;
using System.Text;
using ComponentName.FunctionalDomainNameA.Core.ResourceA;
using ComponentName.FunctionalDomainNameA.Core.ResourceA.Ports;
using ComponentName.FunctionalDomainNameA.Features.CreateResourceA;
using ComponentName.TestsHelpers;

namespace ComponentName.FunctionalDomainNameATests.FunctionalTests;

public class CreateResourceATests(FunctionalTestsApplicationFactory factory)
    : BaseFunctionalTestsCollectionFixture(factory)
{
    private const string RequestPayLoadJson = """{"someProperty":"fakecontent"}""";
    private const string ResponsePayLoadJson = """{"someProperty":"fakecontent"}""";

    [Fact]
    public async Task CreateResourceA_CreateResourceARequestValid_ReturnResourceACreatedResponse()
    {
        // Arrange
        var expectedResponse = DeSerializeData<CreatedResourceAResponse>(ResponsePayLoadJson);

        // Act
        var httpResponse = await SendValidRequest();

        // Assert
        Assert.True(httpResponse.IsSuccessStatusCode);

        var jsonResponse = await httpResponse.Content.ReadAsStringAsync(
            TestContext.Current.CancellationToken
        );
        var response = DeSerializeData<CreatedResourceAResponse>(jsonResponse);

        Assert.Equivalent(expectedResponse, response);
    }

    [Fact]
    public async Task CreateResourceA_CreateResourceARequestValid_SaveInRepository()
    {
        // Act
        var httpResponse = await SendValidRequest();

        // Assert

        var jsonResponse = await httpResponse.Content.ReadAsStringAsync(
            TestContext.Current.CancellationToken
        );
        var response = DeSerializeData<CreateResourceAResponse>(jsonResponse);

        var expectedValueInserted = GetDataById<IResourceARepository, ResourceAEntity, ResourceAId>(
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
            requestUri: $"api/v1/FunctionalDomainNameALowerCase",
            content: jsonContent,
            cancellationToken: TestContext.Current.CancellationToken
        );
    }
}
