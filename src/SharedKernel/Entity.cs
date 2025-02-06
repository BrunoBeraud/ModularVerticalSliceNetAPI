namespace ComponentName.SharedKernel;

public abstract record EntityId
{
    public EntityId(Guid value) => Value = value;

    public EntityId() => Value = Guid.CreateVersion7();

    private Guid Value { get; init; }

    public static implicit operator string(EntityId entityId) => entityId.ToString();

    public override string ToString() => Value.ToString();
}

public abstract class Entity<TId>
    where TId : EntityId
{
    public TId Id { get; init; } = Activator.CreateInstance<TId>();
}
