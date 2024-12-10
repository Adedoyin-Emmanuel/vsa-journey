using Microsoft.EntityFrameworkCore;
using vsa_journey.Domain.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    
namespace vsa_journey.Infrastructure.Persistence.Configurations.Users;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
    }
}