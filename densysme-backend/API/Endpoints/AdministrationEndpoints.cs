using System.Text.Json;
using Core.Constants;
using Core.DataTransfer.Manager;
using Services.Administration;
using static API.Endpoints.RouteConstants;

namespace API.Endpoints;

public static class AdministrationEndpoints
{
    public static WebApplication MapAdministrationEndpoints(this WebApplication app)
    {
        app.MapPost($"{Api}/{Users}/{Employees}", HandleAddUserForStaff).RequireAuthorization(new AuthorizeData(roles: AuthRoleConstants.Admin));
        app.MapPost($"{Api}/{Users}/{Patients}", HandleAddUserForPatient).RequireAuthorization(new AuthorizeData(roles: AuthRoleConstants.Admin));
        app.MapDelete("api/users/{userId:guid}",
                async (IAdministrationService administrationService, Guid userId) =>
                {
                    await administrationService.DeleteUser(userId);
                    return Results.NoContent();
                })
            .RequireAuthorization(new AuthorizeData(AuthRoleConstants.Admin));
        app.MapPost($"{Api}/{Employees}", HandleAddManager).RequireAuthorization(new AuthorizeData(roles: AuthRoleConstants.Admin));
        return app;
    }

    public static async Task<IResult> HandleAddUserForStaff(
        IAdministrationService administrationService,
        Guid employeeId,
        string password,
        int[] roles)
    {
        var userId = await administrationService.AddUserForStaffAsync(employeeId, password, roles);
        return Results.Created($"{Api}/{Users}/{Employees}", userId);
    }

    public static async Task<IResult> HandleAddUserForPatient(
        IAdministrationService administrationService,
        Guid patientId,
        string password)
    {
        var userId = await administrationService.AddUserForPatientAsync(patientId, password);
        return Results.Created($"{Api}/{Users}/{Patients}", userId);
    }

    public static async Task<IResult> HandleAddManager(
        IAdministrationService administrationService,
        IHttpContextAccessor httpContext)
    {
        var photo = httpContext.HttpContext!.Request.Form.Files.SingleOrDefault();
        var request = JsonSerializer.Deserialize<Models.AddManagerRequest>(
            httpContext.HttpContext.Request.Form["request"].Single(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        if (request is null)
            return Results.BadRequest();
        var manager = new AddManagerRequest(
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.IIN,
            request.Phone,
            request.Email,
            request.Address,
            request.DateOfBirth,
            request.YearsOfExperience,
            request.Degree,
            photo);
        var managerId = await administrationService.AddManagerAsync(manager);
        return Results.Created($"{Api}/{Employees}", managerId);
    }
}