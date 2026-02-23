using Microsoft.EntityFrameworkCore;

/*
For every new simple table now you only need:
    Entity
    DTO
    One controller inheriting base
    Optional service (only if business logic exists)
 */

public class CrudService<TEntity> where TEntity : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public CrudService(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    // only override if you need to include related entities or apply filters
    public virtual async Task<List<TEntity>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        => await _dbSet.FindAsync(id);

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public virtual async Task SaveAsync()
        => await _context.SaveChangesAsync();
}
