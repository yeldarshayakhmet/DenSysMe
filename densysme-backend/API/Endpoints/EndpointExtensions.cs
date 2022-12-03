namespace API.Endpoints;

public static class EndpointExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapUserEndpoints();
        app.MapDoctorEndpoints();
        app.MapAdministrationEndpoints();
        app.MapPatientEndpoints();
        app.MapAppointmentEndpoints();
        return app;
    }
}