namespace Core.EntitiesDTOs;

public class BookDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public required string Author { get; set; }
    public int Stock { get; set; }
    public int? CategoryId { get; set; }
}

public class BookDetailDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public required string Author { get; set; }
    public int Stock { get; set; }
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public DateTime CreatedAt { get; set; }

}

