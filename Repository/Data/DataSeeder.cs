using Core.Entities;
using System.Text.Json;

namespace Repository.Data;
public static class DataSeeder
{
    public async static Task SeedDataAsync(AppDbContext _context)
    {
        if (!_context.Categories.Any())
        {
            var categoriesJson = await File.ReadAllTextAsync(@"..\repository\data\JsonSeed\Categories.json");
            var categoriesList = JsonSerializer.Deserialize<List<Category>>(categoriesJson);

            if (categoriesList is not null && categoriesList.Count > 0)
            {
                foreach (var category in categoriesList)
                {
                    category.Id = 0; // Let the DB generate the Id
                }

                await _context.Categories.AddRangeAsync(categoriesList);
                await _context.SaveChangesAsync();
            }
        }

        if (!_context.Books.Any())
        {
            var booksJson = await File.ReadAllTextAsync(@"..\repository\data\JsonSeed\Books.json");
            var booksList = JsonSerializer.Deserialize<List<Book>>(booksJson);
            if (booksList is not null && booksList.Count > 0)
            {
                foreach (var book in booksList)
                {
                    book.Id = 0; // Let the DB generate the Id
                }
                await _context.Books.AddRangeAsync(booksList);
                await _context.SaveChangesAsync();
            }
        }
    }

}
