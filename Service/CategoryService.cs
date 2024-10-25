using Core;
using Core.Entities;

namespace Service;

public class CategoryService : IService<Category>
{
    private readonly IRepository<Category> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Category>();
    }
    public async Task CreateEntityAsync(Category entity)
    {
        await _repository.CreateEntityAsync(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteEntity(Category entity)
    {
        _repository.DeleteEntity(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories;
    }

    public async Task<Category> GetOneByIdAsync(int id)
    {
        var category = await _repository.GetOneByIdAsync(id);
        return category;
    }

    public async Task UpdateEntity(Category entity)
    {
        _repository.UpdateEntity(entity);
        await _unitOfWork.SaveChangesAsync();
    }
}
