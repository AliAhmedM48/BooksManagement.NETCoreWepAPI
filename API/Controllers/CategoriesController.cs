using AutoMapper;
using Core;
using Core.Entities;
using Core.EntitiesDTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class CategoriesController : BaseController
{
    private readonly IService<Category> _service;
    private readonly ILogger<CategoriesController> _logger;
    private readonly IMapper _mapper;

    public CategoriesController(IService<Category> service, ILogger<CategoriesController> logger, IMapper mapper)
    {
        _service = service;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDetailDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCategories()
    {
        _logger.LogInformation("Fetching all categories...");
        var categories = await _service.GetAllAsync();
        var categoryDetailDto = _mapper.Map<IEnumerable<CategoryDetailDto>>(categories);
        return Ok(categoryDetailDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CategoryDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFound), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOneCategory([FromRoute] int? id)
    {
        _logger.LogInformation($"Fetching category with id {id}...");

        if (id is null) return BadRequest("Invalid Id.");
        var category = await _service.GetOneByIdAsync(id.Value);
        if (category is null)
        {
            _logger.LogWarning($"Category with id {id} not found.");
            return NotFound($"Category with id {id} is not found.");
        }
        var categoryDetailDto = _mapper.Map<CategoryDetailDto>(category);
        return Ok(categoryDetailDto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]

    public async Task<IActionResult> CreateOneCategory([FromBody] CategoryDto categoryDto)
    {
        if (categoryDto is null)
        {
            _logger.LogWarning("Invalid category data provided.");
            return BadRequest("Invalid category data.");
        }
        var category = _mapper.Map<Category>(categoryDto);
        await _service.CreateEntityAsync(category);
        _logger.LogInformation("Category created successfully.");
        return CreatedAtAction(nameof(GetOneCategory), new { id = category.Id }, categoryDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFound), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Ok), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateOneCategory([FromRoute] int? id, [FromBody] CategoryDto categoryDto)
    {
        if (categoryDto is null)
        {
            _logger.LogWarning("Invalid category data provided.");
            return BadRequest("Invalid category data.");
        }

        if (id is null)
        {
            _logger.LogWarning("Invalid Id.");
            return BadRequest("Invalid Id");
        }

        var existingCategory = await _service.GetOneByIdAsync(id.Value);

        if (existingCategory is null)
        {
            _logger.LogWarning($"Category with id {id} not found.");
            return NotFound($"Category with id {id} is not found.");
        }

        var updatedCategory = _mapper.Map(categoryDto, existingCategory);
        await _service.UpdateEntity(updatedCategory);
        _logger.LogInformation($"Category with id {id} updated successfully.");
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFound), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(NoContent), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteOneCategory([FromRoute] int? id)
    {
        if (id is null) return BadRequest("Invalid Id");
        var category = await _service.GetOneByIdAsync(id.Value);
        if (category is null)
        {
            _logger.LogWarning($"Category with id {id} not found.");
            return NotFound($"Category with id {id} is not found.");
        }

        await _service.DeleteEntity(category);
        _logger.LogInformation($"Category with id {id} deleted successfully.");

        return NoContent();
    }
}
