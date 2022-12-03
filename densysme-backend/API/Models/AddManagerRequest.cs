using Core.Enums;

namespace API.Models;

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
    AcademicDegree Degree);