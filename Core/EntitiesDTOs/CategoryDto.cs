namespace Core.EntitiesDTOs;

public class CategoryDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}

public class CategoryDetailDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
