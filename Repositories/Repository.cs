namespace NotasAPI.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly NotesContext _context;
    private readonly DbSet<TEntity> _entities;

    protected NotesContext Context => _context;

    public Repository(NotesContext context)
    {
        _context = context;
        _entities = _context.Set<TEntity>();
    }

    public async virtual Task<TEntity> GetByIdAsync(long id)
        => await _entities.Where(entity => entity.Id == id).FirstOrDefaultAsync();

    public async virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        => await _entities.Where(predicate).FirstOrDefaultAsync();

    public async virtual Task<IEnumerable<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> predicate)
        => await _entities.Where(predicate).ToListAsync();

    public async virtual Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        => await _entities.Where(predicate).AnyAsync();

    public virtual Task InsertRangeAsync(IEnumerable<TEntity> entity)
        => _entities.AddRangeAsync(entity);

    public async virtual Task InsertAsync(TEntity entity)
    => await _entities.AddAsync(entity);

    public virtual Task<int> SaveAsync()
        => _context.SaveChangesAsync();

    public async virtual Task<IEnumerable<TEntity>> GetEntitiesAsync()
        => await _entities.ToListAsync();
}
