using Core.Entities;

namespace Core;
public interface IService<TEntity>
    where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetOneByIdAsync(int id);
    Task CreateEntityAsync(TEntity entity);
    Task UpdateEntity(TEntity entity);
    Task DeleteEntity(TEntity entity);
}
