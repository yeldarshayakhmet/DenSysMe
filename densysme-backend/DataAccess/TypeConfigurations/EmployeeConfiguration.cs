using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.TypeConfigurations;

public class EmployeeConfiguration<T> : IndividualConfiguration<T> where T : Employee
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);
        builder.Property(employee => employee.Degree).HasConversion<string>();
    }
}