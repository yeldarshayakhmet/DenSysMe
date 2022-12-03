using System.Security.Claims;
using Core.DataTransfer.Appointment;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Services.Appointment;

public class AppointmentService : IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;

    public AppointmentService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
    {
        _unitOfWork = unitOfWork;
        _httpContext = httpContext;
    }

    public async Task<(Guid?, bool, string)> AddAppointmentAsync(AddAppointmentRequest request)
    {
        var userId = _httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        Core.Entities.Patient? patient = null;
        if (request.Patient is not null)
        {
            patient = await _unitOfWork
                .Collection<Core.Entities.Patient>()
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.IIN == request.Patient.IIN);
            if (patient is null)
            {
                patient = new Core.Entities.Patient
                {
                    FirstName = request.Patient.FirstName,
                    LastName = request.Patient.LastName,
                    IIN = request.Patient.IIN,
                    PhoneNumber = request.Patient.Phone,
                };
                patient = _unitOfWork.Create(patient);
            }
        }
        else if (!string.IsNullOrWhiteSpace(userId))
        {
            var userIdGuid = Guid.Parse(userId);
            patient = await _unitOfWork
                .Collection<Core.Entities.Patient>()
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.UserId.HasValue && p.UserId.Value == userIdGuid);
        }

        if (patient is null)
            return (null, false, "Patient cannot be empty");
        
        var isValidTime = await IsValidTimeAsync(request.DoctorId, request.Time);
        if (!isValidTime)
            return (null, false, "Conflicting appointment time");
        
        var appointment = new Core.Entities.Appointment
        {
            StartTime = request.Time,
            DoctorId = request.DoctorId,
            PatientId = patient.Id,
        };
        appointment = _unitOfWork.Create(appointment);
        await _unitOfWork.SaveAsync();
        
        return (appointment.Id, true, string.Empty);
    }

    private async Task<bool> IsValidTimeAsync(Guid doctorId, DateTime time)
    {
        var oneHour = TimeSpan.FromHours(1);
        return !await _unitOfWork
            .Collection<Core.Entities.Appointment>()
            .AnyAsync(a =>
                a.DoctorId == doctorId
                && a.StartTime - time < oneHour
                && time - a.StartTime < oneHour);
    }

    public async Task UpdateAppointmentAsync(Guid appointmentId, DateTime time)
    {
        var appointment = await _unitOfWork.Collection<Core.Entities.Appointment>().SingleAsync(a => a.Id == appointmentId);
        var isValidTime = await IsValidTimeAsync(appointment.DoctorId, time);
        if (isValidTime)
            appointment.StartTime = time;
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAppointmentAsync(Guid appointmentId)
    {
        var appointment = await _unitOfWork.Collection<Core.Entities.Appointment>().SingleAsync(a => a.Id == appointmentId);
        _unitOfWork.Delete(appointment);
        await _unitOfWork.SaveAsync();
    }

    public async Task<Core.Entities.Appointment[]> GetAppointmentsByDoctor(Guid doctorId) =>
        await _unitOfWork
            .Collection<Core.Entities.Appointment>()
            .Include(appointment => appointment.Doctor)
            .Include(appointment => appointment.Patient)
            .AsNoTracking()
            .Where(appointment => appointment.DoctorId == doctorId)
            .ToArrayAsync();

    public async Task<Core.Entities.Appointment[]> GetAppointmentsByPatient(Guid patientId) =>
        await _unitOfWork
            .Collection<Core.Entities.Appointment>()
            .Include(appointment => appointment.Doctor)
            .Include(appointment => appointment.Patient)
            .AsNoTracking()
            .Where(appointment => appointment.PatientId == patientId)
            .ToArrayAsync();
}