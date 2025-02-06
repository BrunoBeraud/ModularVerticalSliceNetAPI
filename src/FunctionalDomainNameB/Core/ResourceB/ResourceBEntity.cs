using System.Text.Json.Serialization;
using ComponentName.SharedKernel;

namespace ComponentName.FunctionalDomainNameB.Core.ResourceB;

internal record ResourceBId : EntityId
{
    public ResourceBId(Guid value)
        : base(value) { }

    public ResourceBId()
        : base() { }

    public static implicit operator ResourceBId(Guid value) => new(value);

    public override string ToString() => base.ToString();
}

internal class ResourceBEntity : Entity<ResourceBId>
{
    public string SomeProperty { get; init; }

    [JsonConstructor]
    public ResourceBEntity()
    {
        SomeProperty = string.Empty;
    }

    public ResourceBEntity(string someProperty)
        : base()
    {
        SomeProperty = someProperty;
    }

    public ResourceBEntity(ResourceBId id, string someProperty)
    {
        Id = id;
        SomeProperty = someProperty;
    }
}
