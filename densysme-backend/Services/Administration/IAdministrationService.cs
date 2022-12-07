using Core.DataTransfer.Manager;
using Core.Entities;

namespace Services.Administration;

public interface IAdministrationService
{
    Task<Guid> AddUserForStaffAsync(Guid employeeId, string password, int[] roles);
    Task<Guid> AddUserForPatientAsync(Guid patientId, string password);
    Task DeleteUser(Guid userId);
    Task<Guid> AddManagerAsync(AddManagerRequest request);
    Task<Specialization[]> GetSpecializations();
}