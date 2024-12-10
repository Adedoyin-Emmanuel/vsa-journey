using vsa_journey.Domain.Entities.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace vsa_journey.Infrastructure.Persistence.Configurations.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(category => category.Id);

        builder.Property(category => category.Name).IsRequired().HasMaxLength(25);
        
        builder.Property(category => category.CreatedAt).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.Property(category => category.UpdatedAt).IsRequired().ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}