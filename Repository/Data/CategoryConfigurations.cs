using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Data;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Description).IsRequired();
        builder
            .HasMany<Book>()
            .WithOne(p => p.Category)
            .HasForeignKey(P => P.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
