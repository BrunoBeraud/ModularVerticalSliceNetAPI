using System.Text.Json.Serialization;
using ComponentName.SharedKernel;

namespace ComponentName.FunctionalDomainNameA.Core.ResourceA;

internal record ResourceAId : EntityId
{
    public ResourceAId(Guid value)
        : base(value) { }

    public ResourceAId()
        : base() { }

    public static implicit operator ResourceAId(Guid value) => new(value);

    public override string ToString() => base.ToString();
}

internal class ResourceAEntity : Entity<ResourceAId>
{
    public string SomeProperty { get; init; }

    [JsonConstructor]
    public ResourceAEntity()
    {
        SomeProperty = string.Empty;
    }

    public ResourceAEntity(string someProperty)
        : base()
    {
        SomeProperty = someProperty;
    }

    public ResourceAEntity(ResourceAId id, string someProperty)
    {
        Id = id;
        SomeProperty = someProperty;
    }
}
