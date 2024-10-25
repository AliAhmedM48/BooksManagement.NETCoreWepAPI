namespace Core.Entities;

public class Book : BaseEntity
{
    private int stock;
    private decimal price;

    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price
    {
        get => price; set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(Price), "Price must be non-negative");
            price = value;
        }
    }
    public required string Author { get; set; }
    public int Stock
    {
        get => stock;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(Stock), "Stock must be non-negative");
            stock = value;
        }
    }
    public int? CategoryId { get; set; }
    public required Category Category { get; set; }
}
