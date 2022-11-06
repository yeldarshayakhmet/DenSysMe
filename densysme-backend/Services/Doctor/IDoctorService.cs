using Core.DataTransfer.Doctor;

namespace Services.Doctor;

public interface IDoctorService
{
    Task<Guid> AddDoctorAsync(AddDoctorDto doctor, CancellationToken cancellationToken = default);
    Task<DoctorDto[]> GetDoctorsAsync(CancellationToken cancellationToken = default);
}