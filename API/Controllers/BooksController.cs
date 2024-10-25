using AutoMapper;
using Core;
using Core.Entities;
using Core.EntitiesDTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
public class BooksController : BaseController
{
    private readonly IService<Book> _bookService;
    private readonly IService<Category> _categoryService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public BooksController(IService<Book> bookService, IService<Category> categoryService, ILogger<BooksController> logger, IMapper mapper)
    {
        _bookService = bookService;
        _categoryService = categoryService;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookDetailDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBooks()
    {
        _logger.LogInformation("Fetching all books...");
        var books = await _bookService.GetAllAsync();
        var bookDetailDtos = _mapper.Map<IEnumerable<BookDetailDto>>(books);
        return Ok(bookDetailDtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFound), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOneBook([FromRoute] int? id)
    {
        _logger.LogInformation($"Fetching Book with id {id}...");

        if (id is null || id < 0) return BadRequest("Invalid Id.");
        var book = await _bookService.GetOneByIdAsync(id.Value);
        if (book is null)
        {
            _logger.LogWarning($"Book with id {id} not found.");
            return NotFound($"Book with id {id} is not found.");
        }
        var bookDetailDto = _mapper.Map<BookDetailDto>(book);
        return Ok(bookDetailDto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]

    public async Task<IActionResult> CreateOneBook([FromBody] BookDto bookDto)
    {
        if (bookDto is null)
        {
            _logger.LogWarning("Invalid book data provided.");
            return BadRequest("Invalid book data provided.");
        }

        try
        {
            var category = await _categoryService.GetOneByIdAsync(bookDto.CategoryId.Value);
            if (category is null)
            {
                _logger.LogWarning($"Category with ID {bookDto.CategoryId} not found.");
                return BadRequest($"Category with ID {bookDto.CategoryId} does not exist.");
            }

            var book = _mapper.Map<Book>(bookDto);
            await _bookService.CreateEntityAsync(book);
            _logger.LogInformation("Book created successfully.");
            return CreatedAtAction(nameof(GetOneBook), new { id = book.Id }, bookDto);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"An error occurred while saving the book with title '{bookDto.Name}'.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the book.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while creating the book.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFound), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Ok), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateOneBook([FromRoute] int? id, [FromBody] BookDto bookDto)
    {
        if (bookDto is null)
        {
            _logger.LogWarning("Invalid book data provided.");
            return BadRequest("Invalid book data.");
        }

        if (id is null)
        {
            _logger.LogWarning("Invalid Id.");
            return BadRequest("Invalid Id");
        }

        var existingBook = await _bookService.GetOneByIdAsync(id.Value);

        if (existingBook is null)
        {
            _logger.LogWarning($"Book with id {id} not found.");
            return NotFound($"Book with id {id} is not found.");
        }

        var updatedBook = _mapper.Map(bookDto, existingBook);
        await _bookService.UpdateEntity(updatedBook);
        _logger.LogInformation($"Book with id {id} updated successfully.");
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFound), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(NoContent), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteOneBook([FromRoute] int? id)
    {
        if (id is null) return BadRequest("Invalid Id");
        var book = await _bookService.GetOneByIdAsync(id.Value);
        if (book is null)
        {
            _logger.LogWarning($"Book with id {id} not found.");
            return NotFound($"Book with id {id} is not found.");
        }

        await _bookService.DeleteEntity(book);
        _logger.LogInformation($"Book with id {id} deleted successfully.");

        return NoContent();
    }
}
