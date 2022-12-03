namespace Core.DataTransfer.Appointment;

public record AddAppointmentRequest(Guid DoctorId, PatientDto? Patient, DateTime Time);

public record PatientDto(string FirstName, string LastName, string IIN, string Phone);