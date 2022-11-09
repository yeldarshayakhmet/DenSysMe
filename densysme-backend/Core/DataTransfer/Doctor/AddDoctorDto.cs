using Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Core.DataTransfer.Doctor;

public record AddDoctorDto(
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
    IFormFile? Photo,
    string Password);