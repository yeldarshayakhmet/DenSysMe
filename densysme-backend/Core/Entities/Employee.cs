using Core.Enums;

namespace Core.Entities;

public abstract class Employee : Individual
{
    public byte[]? Photo { get; set; }
    public int YearsOfExperience { get; set; }
    public AcademicDegree Degree { get; set; }
}