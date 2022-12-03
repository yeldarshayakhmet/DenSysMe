namespace Core.Entities;

public class Appointment
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }
}