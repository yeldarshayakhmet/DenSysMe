namespace Core.Entities;

public class Manager : Employee
{
    public Guid? DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
}