using Core;
using Core.Entities;
using Microsoft.Extensions.Logging;
using Repository.Data;
using System.Collections;

namespace Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly ILogger _logger;
    private readonly Hashtable _repositories;

    public UnitOfWork(AppDbContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _repositories = new Hashtable();
        _logger = logger;
    }

    public void Dispose()
    {
        _context.Dispose();
        _logger.LogInformation("Dispose AppDbContext");
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;
        if (!_repositories.ContainsKey(type))
        {
            _logger.LogInformation($"Repository Created Type {type}");
            var repo = new Repository<TEntity>(_context);
            _repositories.Add(type, repo);
            return repo;
        }
        return _repositories[type] as IRepository<TEntity>;

    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
