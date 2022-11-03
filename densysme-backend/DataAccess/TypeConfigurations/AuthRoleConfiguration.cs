using Core.Constants;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.TypeConfigurations;

public class AuthRoleConfiguration : IEntityTypeConfiguration<AuthRole>
{
    public void Configure(EntityTypeBuilder<AuthRole> builder)
    {
        builder
            .HasMany(role => role.Users)
            .WithMany(user => user.AuthRoles);
        
        builder.HasData(new List<AuthRole>
        {
            new() { Id = 1, Name = AuthRoleConstants.Admin },
            new() { Id = 2, Name = AuthRoleConstants.Doctor },
            new() { Id = 3, Name = AuthRoleConstants.Patient }
        });
    }
}