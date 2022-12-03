using Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Core.DataTransfer.Manager;

public record AddManagerRequest(
    string FirstName,
    string MiddleName,
    string LastName,
    string IIN,
    string Phone,
    string Email,
    string Address,
    DateTime DateOfBirth,
    int YearsOfExperience,
    AcademicDegree Degree,
    IFormFile? Photo);