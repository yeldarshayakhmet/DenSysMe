using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.TypeConfigurations;

public class PatientConfiguration : IndividualConfiguration<Patient>
{
    public override void Configure(EntityTypeBuilder<Patient> builder)
    {
        base.Configure(builder);
        builder.Property(patient => patient.BloodType).HasConversion<string>();
        builder.Property(patient => patient.MaritalStatus).HasConversion<string>();
        builder.Property(patient => patient.RegistrationDate).HasColumnType(DataConstants.DateTime);
    }
}