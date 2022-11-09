using Core.Enums;

namespace Core.Entities;

public class Doctor : Employee
{
    public decimal AppointmentPrice { get; set; }
    public double Rating { get; set; }
    public MedicalCategory Category { get; set; }
}