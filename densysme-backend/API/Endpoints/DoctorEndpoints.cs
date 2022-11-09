using System.Text.Json;
using API.Models;
using Core.DataTransfer.Doctor;
using Services.Doctor;
using static API.Endpoints.RouteConstants;

namespace API.Endpoints;

public static class DoctorEndpoints
{
    public static WebApplication MapDoctorEndpoints(this WebApplication app)
    {
        app.MapPost($"{Api}/{Doctors}", HandleAddDoctor).AllowAnonymous();
        app.MapGet($"{Api}/{Doctors}", HandleGetDoctors)
            .Produces<DoctorDto[]>()
            .RequireAuthorization();
        return app;
    }

    public static async Task<IResult> HandleAddDoctor(
        IDoctorService doctorService,
        HttpContext httpContext)
    {
        var photo = httpContext.Request.Form.Files.SingleOrDefault();
        var request = JsonSerializer.Deserialize<AddDoctorRequest>(
            httpContext.Request.Form["request"].Single(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        if (request is null)
            return Results.BadRequest();
        var doctorData = new AddDoctorDto(
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.IIN,
            request.Phone,
            request.Email,
            request.Address,
            request.DateOfBirth,
            request.YearsOfExperience,
            request.Category,
            request.AppointmentPrice,
            request.Degree,
            photo,
            request.Password);
        
        var doctorId = await doctorService.AddDoctorAsync(doctorData);

        return Results.Accepted(value: doctorId);
    }

    public static async Task<IResult> HandleGetDoctors(IDoctorService doctorService)
    {
        var doctors = await doctorService.GetDoctorsAsync();
        return Results.Ok(doctors);
    }
}