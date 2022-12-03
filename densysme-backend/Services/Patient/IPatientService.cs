using Core.DataTransfer.Patient;

namespace Services.Patient;

public interface IPatientService
{
    Task<Guid> AddPatientAsync(AddPatientRequest request);
    Task UpdatePatientAsync(UpdatePatientRequest request);
    Task<Core.Entities.Patient[]> GetPatientsAsync(string search);
}