using System.Text.Json;

using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using SharedKernel;

namespace TestsHelpers;

// Class fixture = Test containers shared for all tests within a class tests
public abstract class BaseFunctionalTestsClassFixture(FunctionalTestsApplicationFactory factory)
    : BaseFunctionalTests(factory),
     IClassFixture<FunctionalTestsApplicationFactory>
{ }

// Collection fixture = Test containers shared for all tests
[Collection(nameof(BaseFunctionalTestsCollectionFixture))]
public abstract class BaseFunctionalTestsCollectionFixture(FunctionalTestsApplicationFactory factory)
    : BaseFunctionalTests(factory)
{ }

public abstract class BaseFunctionalTests(FunctionalTestsApplicationFactory factory)
{
    protected HttpClient Client { get; init; } = factory.CreateClient();
    protected FunctionalTestsApplicationFactory Factory { get; init; } = factory;
    protected JsonSerializerOptions JsonOptions { get; init; } = factory.Services
        .GetRequiredService<IOptions<JsonOptions>>()
        .Value
        .SerializerOptions;

    protected string SerializeData<TData>(TData dataToSerialize) => JsonSerializer.Serialize(dataToSerialize, JsonOptions);
    protected TData DeSerializeData<TData>(string dataToDeSerialize) => JsonSerializer.Deserialize<TData>(dataToDeSerialize, JsonOptions)!;


    protected void SeedData<TRepository, TData, TEntityId>(TData dataToSeed)
        where TEntityId : EntityId
        where TData : Entity<TEntityId>
        where TRepository : IRepository<TData, TEntityId>
    {
        var repository = Factory.Services.GetRequiredService<TRepository>();

        repository.Create(dataToSeed);
    }

    protected TData? GetDataById<TRepository, TData, TEntityId>(TEntityId id)
        where TEntityId : EntityId
        where TData : Entity<TEntityId>
        where TRepository : IRepository<TData, TEntityId>
    {
        var repository = Factory.Services.GetRequiredService<TRepository>();

        return repository.GetById(id);
    }

}
