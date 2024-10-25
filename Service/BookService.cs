using Core;
using Core.Entities;

namespace Service;
public class BookService : IService<Book>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Book> _repository;

    public BookService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Book>();
    }
    public async Task CreateEntityAsync(Book entity)
    {
        await _repository.CreateEntityAsync(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteEntity(Book entity)
    {
        _repository.DeleteEntity(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        var books = await _repository.GetAllAsync();
        return books;
    }

    public async Task<Book> GetOneByIdAsync(int id)
    {
        var book = await _repository.GetOneByIdAsync(id);

        return book;
    }

    public async Task UpdateEntity(Book entity)
    {
        _repository.UpdateEntity(entity);
        await _unitOfWork.SaveChangesAsync();
    }
}
