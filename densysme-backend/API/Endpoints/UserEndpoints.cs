using Core.DataTransfer.User;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.User;
using static API.Endpoints.RouteConstants;

namespace API.Endpoints;

public static class UserEndpoints
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        app.MapPost($"{Api}/{Employees}/login", HandleLogin<Employee>);
        app.MapPost($"{Api}/{Patients}/login", HandleLogin<Patient>);
        return app;
    }

    public static async Task<IResult> HandleLogin<T>(
        [FromServices] IRequestValidator<LoginDto> validator,
        [FromServices] IUserService userService,
        [FromBody] LoginDto loginData) where  T : Individual
    {
        var validationResult = await validator.Validate(loginData);
        if (validationResult.Any())
            return Results.BadRequest(validationResult);
        
        var authResult = await userService.AuthenticateAsync<T>(loginData);

        return authResult.Successful ? Results.Ok(authResult) : Results.Unauthorized();
    }
}