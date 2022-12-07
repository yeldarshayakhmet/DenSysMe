using Core.DataTransfer.Doctor;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Services.Doctor;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;

    public DoctorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> AddDoctorAsync(AddDoctorRequest doctor, CancellationToken cancellationToken = default)
    {
        var dbDoctor = new Core.Entities.Doctor
        {
            Id = default,
            FirstName = doctor.FirstName,
            MiddleName = doctor.MiddleName,
            LastName = doctor.LastName,
            IIN = doctor.IIN,
            Email = doctor.Email,
            PhoneNumber = doctor.Phone,
            Address = doctor.Address,
            DateOfBirth = doctor.DateOfBirth,
            Photo = doctor.Photo is not null ? await Helpers.GetFileBytes(doctor.Photo, cancellationToken) : null,
            YearsOfExperience = doctor.YearsOfExperience,
            Degree = doctor.Degree,
            Category = doctor.Category,
            AppointmentPrice = doctor.AppointmentPrice,
            SpecializationId = doctor.SpecializationId
        };

        dbDoctor = _unitOfWork.Create(dbDoctor);
        await _unitOfWork.SaveAsync(cancellationToken);
        return dbDoctor.Id;
    }

    public async Task UpdateDoctorAsync(UpdateDoctorRequest request, CancellationToken cancellationToken = default)
    {
        var doctor = await _unitOfWork.Collection<Core.Entities.Doctor>().SingleAsync(doctor => doctor.Id == request.Id, cancellationToken);
        doctor.FirstName = request.FirstName;
        doctor.MiddleName = request.MiddleName;
        doctor.LastName = request.LastName;
        doctor.IIN = request.IIN;
        doctor.Email = request.Email;
        doctor.PhoneNumber = request.Phone;
        doctor.Address = request.Address;
        doctor.DateOfBirth = request.DateOfBirth;
        doctor.Photo = request.Photo is not null ? await Helpers.GetFileBytes(request.Photo, cancellationToken) : null;
        doctor.YearsOfExperience = request.YearsOfExperience;
        doctor.Degree = request.Degree;
        doctor.Category = request.Category;
        doctor.AppointmentPrice = request.AppointmentPrice;
        doctor.SpecializationId = request.SpecializationId;

        await _unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task<Core.Entities.Doctor[]> GetDoctorsAsync(string? search, Guid[]? specializations, CancellationToken cancellationToken = default) => await _unitOfWork
        .Collection<Core.Entities.Doctor>()
        .Include(doctor => doctor.Specialization)
        .AsNoTracking()
        .Where(doctor => string.IsNullOrWhiteSpace(search)
            || EF.Functions.Like(doctor.FirstName, $"%{search}%") 
            || EF.Functions.Like(doctor.LastName, $"%{search}%"))
        .Where(doctor =>
            specializations == null
            || !specializations.Any()
            || doctor.SpecializationId.HasValue && specializations.Contains(doctor.SpecializationId.Value))
        .ToArrayAsync(cancellationToken);
}