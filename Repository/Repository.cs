using Core;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Repository;
internal class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateEntityAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public void DeleteEntity(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        if (typeof(TEntity) == typeof(Book))
        {
            return (IEnumerable<TEntity>)await _context.Books.Include(p => p.Category).ToListAsync();
        }
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetOneByIdAsync(int id)
    {
        if (typeof(TEntity) == typeof(Book))
        {
            return await _context.Books.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id) as TEntity;
        }
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public void UpdateEntity(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }
}
