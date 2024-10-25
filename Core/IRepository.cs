using Core.Entities;

namespace Core;
public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetOneByIdAsync(int id);
    Task CreateEntityAsync(TEntity entity);
    void UpdateEntity(TEntity entity);
    void DeleteEntity(TEntity entity);

}
