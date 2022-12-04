using Core.DataTransfer.Doctor;

namespace Services.Doctor;

public interface IDoctorService
{
    Task<Guid> AddDoctorAsync(AddDoctorRequest doctor, CancellationToken cancellationToken = default);
    Task UpdateDoctorAsync(UpdateDoctorRequest request, CancellationToken cancellationToken = default);
    Task<Core.Entities.Doctor[]> GetDoctorsAsync(string? search, Guid[]? specializations, CancellationToken cancellationToken = default);
}