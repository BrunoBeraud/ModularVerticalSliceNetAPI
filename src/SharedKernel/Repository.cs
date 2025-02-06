namespace ComponentName.SharedKernel;

public interface IRepository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : EntityId
{
    TEntity? GetById(TEntityId id);
    void Create(TEntity entityAToAdd);
}
