using vsa_journey.Domain.Entities.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace vsa_journey.Infrastructure.Persistence.Configurations.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(category => category.Id);

        builder.Property(category => category.Name).IsRequired().HasMaxLength(30);

        builder.HasIndex(category => category.Name).IsUnique();
        
        builder.Property(category => category.CreatedAt).ValueGeneratedOnAdd();
        
        builder.Property(category => category.UpdatedAt).ValueGeneratedOnAddOrUpdate();
    }
}