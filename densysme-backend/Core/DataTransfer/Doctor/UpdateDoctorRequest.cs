using Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Core.DataTransfer.Doctor;

public record UpdateDoctorRequest(
    Guid Id,
    string FirstName,
    string MiddleName,
    string LastName,
    string IIN,
    string Phone,
    string Email,
    string Address,
    DateTime DateOfBirth,
    int YearsOfExperience,
    MedicalCategory Category,
    decimal AppointmentPrice,
    AcademicDegree Degree,
    Guid SpecializationId,
    IFormFile? Photo);