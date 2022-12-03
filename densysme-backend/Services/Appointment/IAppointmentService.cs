using Core.DataTransfer.Appointment;

namespace Services.Appointment;

public interface IAppointmentService
{
    Task<(Guid?, bool, string)> AddAppointmentAsync(AddAppointmentRequest request);
    Task UpdateAppointmentAsync(Guid appointmentId, DateTime time);
    Task DeleteAppointmentAsync(Guid appointmentId);
    Task<Core.Entities.Appointment[]> GetAppointmentsByDoctor(Guid doctorId);
    Task<Core.Entities.Appointment[]> GetAppointmentsByPatient(Guid patientId);
}