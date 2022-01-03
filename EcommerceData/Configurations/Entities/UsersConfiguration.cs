using EcommerceData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceData.Configurations.Entities
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("Password@1"),
                    PhoneNumber = "860855183",
                    FirstName = "Lukas",
                    LastName = "Songulija",
                    TypeId = 1
                });
        }
    }
}
