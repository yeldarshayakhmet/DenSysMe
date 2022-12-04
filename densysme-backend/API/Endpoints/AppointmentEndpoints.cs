using Core.DataTransfer.Appointment;
using Services.Appointment;
using static API.Endpoints.RouteConstants;

namespace API.Endpoints;

public static class AppointmentEndpoints
{
    public static WebApplication MapAppointmentEndpoints(this WebApplication app)
    {
        app.MapPost($"{Api}/appointments", HandleAddAppointment).AllowAnonymous();
        app.MapPut($"{Api}/appointments", HandleUpdateAppointment).RequireAuthorization();
        app.MapDelete($"{Api}/appointments", HandleDeleteAppointment).RequireAuthorization();
        app.MapGet($"{Api}/doctors/appointments",
            async (IAppointmentService appointmentService, Guid doctorId) =>
                await appointmentService.GetAppointmentsByDoctor(doctorId))
            .RequireAuthorization();
        app.MapGet($"{Api}/patients/appointments",
            async (IAppointmentService appointmentService, Guid patientId) =>
                await appointmentService.GetAppointmentsByPatient(patientId))
            .RequireAuthorization();
        return app;
    }

    public static async Task<IResult> HandleAddAppointment(
        IAppointmentService appointmentService,
        AddAppointmentRequest request)
    {
        var (appointmentId, successful, message) = await appointmentService.AddAppointmentAsync(request);
        return !successful ? Results.BadRequest(message) : Results.Created($"{Api}/appointments", appointmentId);
    }
    
    public static async Task<IResult> HandleUpdateAppointment(
        IAppointmentService appointmentService,
        Guid appointmentId,
        DateTime time)
    {
        await appointmentService.UpdateAppointmentAsync(appointmentId, time);
        return Results.Accepted();
    }

    public static async Task<IResult> HandleDeleteAppointment(
        IAppointmentService appointmentService,
        Guid appointmentId)
    {
        await appointmentService.DeleteAppointmentAsync(appointmentId);
        return Results.NoContent();
    }
}