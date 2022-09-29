using NotasAPI.DataAccess;

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

    public virtual Task InsertRangeAsync(IEnumerable<TEntity> entity)
     => _entities.AddRangeAsync(entity);

    public virtual Task<int> SaveAsync()
    => _context.SaveChangesAsync();
}
