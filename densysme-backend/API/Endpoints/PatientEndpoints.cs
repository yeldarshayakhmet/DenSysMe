using Core.Constants;
using Core.DataTransfer.Patient;
using Services.Patient;
using static API.Endpoints.RouteConstants;

namespace API.Endpoints;

public static class PatientEndpoints
{
    public static WebApplication MapPatientEndpoints(this WebApplication app)
    {
        app.MapPut($"{Api}/{Patients}", HandleUpdatePatient).RequireAuthorization();
        app.MapGet($"{Api}/{Patients}", HandleGetPatients)
            .RequireAuthorization(new AuthorizeData(roles: $"{AuthRoleConstants.Admin}, {AuthRoleConstants.Doctor}"));
        return app;
    }

    public static async Task<IResult> HandleUpdatePatient(IPatientService patientService, UpdatePatientRequest request)
    {
        await patientService.UpdatePatientAsync(request);
        return Results.Accepted($"{Api}/{Patients}");
    }

    public static async Task<IResult> HandleGetPatients(IPatientService patientService, string search)
    {
        var patients = await patientService.GetPatientsAsync(search);
        return Results.Ok(patients);
    }
}