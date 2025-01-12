using FunctionalDomainNameA.Core.ResourceA.Ports;
using FunctionalDomainNameA.Core.ResourceA;

namespace FunctionalDomainNameA;

internal class ResourceARepositoryInMemory : IResourceARepository
{
    private readonly List<ResourceAEntity> _inMemoryCollection = [
        new ResourceAEntity(
            id: Guid.Parse("01940e59-db41-7915-8b8e-3670db6ed5bd"),
            someProperty: "fake seed")];

    public void Create(ResourceAEntity entityAToAdd)
    {
        _inMemoryCollection.Add(entityAToAdd);
    }

    public ResourceAEntity? GetById(ResourceAId id)
    {
        return _inMemoryCollection.SingleOrDefault(entityA => entityA.Id == id);
    }
}
