using Core.DataTransfer.Manager;
using Core.Entities;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services.User;

namespace Services.Administration;

public class AdministrationService : IAdministrationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;

    public AdministrationService(IUnitOfWork unitOfWork, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
    }

    public async Task<Guid> AddUserForStaffAsync(Guid employeeId, string password, int[] roles)
    {
        var manager = await _unitOfWork.Collection<Manager>().FirstOrDefaultAsync(manager => manager.Id == employeeId);
        var doctor = await _unitOfWork.Collection<Core.Entities.Doctor>().FirstOrDefaultAsync(doctor => doctor.Id == employeeId);
        var dbRoles = await _unitOfWork.Collection<AuthRole>().Where(role => roles.Contains(role.Id)).ToListAsync();
        Guid userId;
        if (manager is not null)
        {
            userId = await _userService.RegisterAsync(password, dbRoles, CancellationToken.None);
            manager.UserId = userId;
        }
        else if (doctor is not null)
        {
            userId = await _userService.RegisterAsync(password, dbRoles, CancellationToken.None);
            doctor.UserId = userId;
        }
        else
            throw new ApplicationException("not found");

        await _unitOfWork.SaveAsync();
        return userId;
    }

    public async Task<Guid> AddUserForPatientAsync(Guid patientId, string password)
    {
        var patient = await _unitOfWork.Collection<Core.Entities.Patient>().SingleAsync(patient => patient.Id == patientId);
        var userId = await _userService.RegisterAsync(password, Enumerable.Empty<AuthRole>(), CancellationToken.None);
        patient.UserId = userId;
        return userId;
    }

    public async Task DeleteUser(Guid userId)
    {
        var user = await _unitOfWork.Collection<Core.Entities.User>().SingleAsync(user => user.Id == userId);
        _unitOfWork.Delete(user);
        await _unitOfWork.SaveAsync();
    }

    public async Task<Guid> AddManagerAsync(AddManagerRequest request)
    {
        var manager = new Manager
        {
            Id = default,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            IIN = request.IIN,
            Email = request.Email,
            PhoneNumber = request.Phone,
            Address = request.Address,
            DateOfBirth = request.DateOfBirth,
            Photo = request.Photo is not null ? await Helpers.GetFileBytes(request.Photo) : null,
            YearsOfExperience = request.YearsOfExperience,
            Degree = request.Degree,
        };

        manager = _unitOfWork.Create(manager);
        await _unitOfWork.SaveAsync();
        return manager.Id;
    }
}