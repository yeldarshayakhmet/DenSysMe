using System.Text.Json;
using Core.Constants;
using Core.DataTransfer.Doctor;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Doctor;
using static API.Endpoints.RouteConstants;
using AddDoctorRequest = Core.DataTransfer.Doctor.AddDoctorRequest;

namespace API.Endpoints;

public static class DoctorEndpoints
{
    public static WebApplication MapDoctorEndpoints(this WebApplication app)
    {
        app.MapPost($"{Api}/{Doctors}", HandleAddDoctor).RequireAuthorization(new AuthorizeData(roles: AuthRoleConstants.Admin));
        app.MapPut($"{Api}/{Doctors}", HandleUpdateDoctor).RequireAuthorization(new AuthorizeData(roles: AuthRoleConstants.Admin));
        app.MapGet($"{Api}/{Doctors}", HandleGetDoctors)
            .Produces<Doctor[]>()
            .RequireAuthorization();
        return app;
    }

    public static async Task<IResult> HandleAddDoctor(
        IDoctorService doctorService,
        IHttpContextAccessor httpContext)
    {
        var photo = httpContext.HttpContext!.Request.Form.Files.SingleOrDefault();
        var request = JsonSerializer.Deserialize<Models.AddDoctorRequest>(
            httpContext.HttpContext.Request.Form["request"].Single(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        if (request is null)
            return Results.BadRequest();
        var doctorData = new AddDoctorRequest(
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
            request.SpecializationId,
            photo);
        
        var doctorId = await doctorService.AddDoctorAsync(doctorData);

        return Results.Created($"{Api}/{Doctors}", doctorId);
    }

    public static async Task<IResult> HandleUpdateDoctor(
        IDoctorService doctorService,
        IHttpContextAccessor httpContext)
    {
        var photo = httpContext.HttpContext!.Request.Form.Files.SingleOrDefault();
        var request = JsonSerializer.Deserialize<Models.UpdateDoctorRequest>(
            httpContext.HttpContext.Request.Form["request"].Single(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        if (request is null)
            return Results.BadRequest();
        var doctorData = new UpdateDoctorRequest(
            request.Id,
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
            request.SpecializationId,
            photo);

        await doctorService.UpdateDoctorAsync(doctorData);
        return Results.Accepted();
    }

    public static async Task<IResult> HandleGetDoctors(IDoctorService doctorService, string? search = null, [FromBody] Guid[]? specializations = null)
    {
        var doctors = await doctorService.GetDoctorsAsync(search, specializations);
        return Results.Ok(doctors);
    }
}