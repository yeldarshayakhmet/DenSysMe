using Core.DataTransfer.Manager;

namespace Services.Administration;

public interface IAdministrationService
{
    Task<Guid> AddUserForStaffAsync(Guid employeeId, string password, int[] roles);
    Task<Guid> AddUserForPatientAsync(Guid patientId, string password);
    Task DeleteUser(Guid userId);
    Task<Guid> AddManagerAsync(AddManagerRequest request);
}