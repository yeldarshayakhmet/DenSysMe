using Core.DataTransfer.Doctor;
using Microsoft.AspNetCore.Mvc;
using Services.Doctor;
using static API.Endpoints.RouteConstants;

namespace API.Endpoints;

public static class DoctorEndpoints
{
    public static WebApplication MapDoctorEndpoints(this WebApplication app)
    {
        app.MapPost($"{Api}/{Doctors}", HandleAddDoctor).RequireAuthorization().Accepts<IFormFile>("image/x-png");
        app.MapGet($"{Api}/{Doctors}", HandleGetDoctors).Produces<DoctorDto[]>().RequireAuthorization();
        return app;
    }

    public static async Task<IResult> HandleAddDoctor(
        [FromServices] IDoctorService doctorService,
        [FromBody] AddDoctorDto request)
    {
        var doctorId = await doctorService.AddDoctorAsync(request);

        return Results.Ok(doctorId);
    }

    public static async Task<IResult> HandleGetDoctors([FromServices] IDoctorService doctorService)
    {
        var doctors = await doctorService.GetDoctorsAsync();
        return Results.Ok(doctors);
    }
}