using Microsoft.EntityFrameworkCore;
using vsa_journey.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vsa_journey.Domain.Entities.Category;

namespace vsa_journey.Infrastructure.Persistence.Configurations.Products;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);

        builder.Property(product => product.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(product => product.Description)
            .IsRequired()
            .HasMaxLength(1500);

        builder.Property(product => product.Price)
            .IsRequired();

        builder.Property(product => product.BaseImageUrl).
            IsRequired();
        
        builder.Property(product => product.Quantity)
            .IsRequired();

        builder.HasOne(product => product.Category)
            .WithMany()
            .HasForeignKey(product => product.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(product => product.CreatedAt)
            .ValueGeneratedOnAdd();

        builder.Property(product => product.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate();
    }
}