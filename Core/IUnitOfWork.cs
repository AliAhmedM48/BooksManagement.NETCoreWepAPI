using Core.Entities;

namespace Core;
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
}
