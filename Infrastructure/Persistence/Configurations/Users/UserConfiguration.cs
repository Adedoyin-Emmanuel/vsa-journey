using Microsoft.EntityFrameworkCore;
using vsa_journey.Domain.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    
namespace vsa_journey.Infrastructure.Persistence.Configurations.Users;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.Property(user => user.FirstName).IsRequired().HasMaxLength(20);

        builder.Property(user => user.LastName).IsRequired().HasMaxLength(20);

        builder.HasIndex(user => user.Email).IsUnique();

        builder.Property(user => user.CreatedAt).IsRequired().ValueGeneratedOnAdd()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(user => user.UpdatedAt).IsRequired().ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

    }
}