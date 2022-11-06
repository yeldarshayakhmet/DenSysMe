namespace Core.Entities;

public class Doctor : Employee
{
    public decimal AppointmentPrice { get; set; }
    public double Rating { get; set; }
}