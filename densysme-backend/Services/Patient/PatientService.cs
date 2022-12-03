using Core.DataTransfer.Patient;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Services.Patient;

public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;

    public PatientService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> AddPatientAsync(AddPatientRequest request)
    {
        var patient = new Core.Entities.Patient
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
            MaritalStatus = request.MaritalStatus,
            BloodType = request.BloodType,
            RegistrationDate = DateTime.UtcNow,
        };

        var dbPatient = _unitOfWork.Create(patient);
        await _unitOfWork.SaveAsync();
        return dbPatient.Id;
    }

    public async Task UpdatePatientAsync(UpdatePatientRequest request)
    {
        var patient = await _unitOfWork.Collection<Core.Entities.Patient>()
            .SingleAsync(patient => patient.Id == request.Id);

        patient.FirstName = request.FirstName;
        patient.LastName = request.LastName;
        patient.MiddleName = request.MiddleName;
        patient.IIN = request.IIN;
        patient.Email = request.Email;
        patient.PhoneNumber = request.Phone;
        patient.Address = request.Address;
        patient.DateOfBirth = request.DateOfBirth;
        patient.MaritalStatus = request.MaritalStatus;
        patient.BloodType = request.BloodType;
        patient.RegistrationDate = DateTime.UtcNow;

        await _unitOfWork.SaveAsync();
    }

    public async Task<Core.Entities.Patient[]> GetPatientsAsync(string search)
    {
        var pattern = $"%{search}%";
        return await _unitOfWork
            .Collection<Core.Entities.Patient>()
            .AsNoTracking()
            .Where(patient =>
                EF.Functions.Like(patient.FirstName, pattern)
                || EF.Functions.Like(patient.LastName, pattern))
            .ToArrayAsync();
    }
}