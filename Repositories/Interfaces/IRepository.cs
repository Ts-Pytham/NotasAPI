namespace NotasAPI.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity> GetByIdAsync(long id);
    Task InsertRangeAsync(IEnumerable<TEntity> entity);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetEntitiesAsync();
    Task<int> SaveAsync();
}
