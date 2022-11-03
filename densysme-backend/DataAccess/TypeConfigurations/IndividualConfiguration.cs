using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.TypeConfigurations;

public class IndividualConfiguration<T> : IEntityTypeConfiguration<T> where T : Individual
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasOne(individual => individual.User).WithOne();
        builder.HasIndex(individual => individual.FirstName);
        builder.HasIndex(individual => individual.LastName);
        builder.HasIndex(individual => individual.IIN);
        builder.HasIndex(individual => individual.PhoneNumber);
        builder.HasIndex(individual => individual.Email);
    }
}