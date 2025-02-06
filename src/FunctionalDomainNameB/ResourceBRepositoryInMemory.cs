using ComponentName.FunctionalDomainNameB.Core.ResourceB;
using ComponentName.FunctionalDomainNameB.Core.ResourceB.Ports;

namespace ComponentName.FunctionalDomainNameB;

internal class ResourceBRepositoryInMemory : IResourceBRepository
{
    private readonly List<ResourceBEntity> _inMemoryCollection =
    [
        new ResourceBEntity(
            id: Guid.Parse("01940e59-db41-7915-8b8e-3670db6ed5bd"),
            someProperty: "fake seed"
        ),
    ];

    public void Create(ResourceBEntity entityAToAdd)
    {
        _inMemoryCollection.Add(entityAToAdd);
    }

    public ResourceBEntity? GetById(ResourceBId id)
    {
        return _inMemoryCollection.SingleOrDefault(entityA => entityA.Id == id);
    }
}
