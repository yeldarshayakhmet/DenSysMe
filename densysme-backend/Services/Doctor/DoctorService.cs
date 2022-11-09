using Core.Constants;
using Core.DataTransfer.Doctor;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services.User;

namespace Services.Doctor;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;

    public DoctorService(IUnitOfWork unitOfWork, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
    }

    public async Task<Guid> AddDoctorAsync(AddDoctorDto doctor, CancellationToken cancellationToken = default)
    {
        using var imageStream = new MemoryStream();
        if (doctor.Photo is not null)
            await doctor.Photo.CopyToAsync(imageStream, cancellationToken);

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
            DateOfBirth = DateOnly.FromDateTime(doctor.DateOfBirth),
            Photo = imageStream.Length > 0 ? imageStream.ToArray() : null,
            YearsOfExperience = doctor.YearsOfExperience,
            Degree = doctor.Degree,
            AppointmentPrice = doctor.AppointmentPrice
        };

        dbDoctor = _unitOfWork.Create(dbDoctor);
        await _unitOfWork.SaveAsync(cancellationToken);
        var userId = await _userService.RegisterAsync(doctor.Password, new List<string> { AuthRoleConstants.Doctor }, cancellationToken);
        dbDoctor.UserId = userId;
        await _unitOfWork.SaveAsync(cancellationToken);
        return dbDoctor.Id;
    }

    public async Task<DoctorDto[]> GetDoctorsAsync(CancellationToken cancellationToken = default) => await _unitOfWork
        .Collection<Core.Entities.Doctor>()
        .Select(doctor => new DoctorDto(doctor.FirstName, doctor.LastName, doctor.PhoneNumber, doctor.Photo))
        .ToArrayAsync(cancellationToken);
}