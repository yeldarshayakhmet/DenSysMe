using Core.Enums;

namespace Core.DataTransfer.Patient;

public record AddPatientRequest(
    string FirstName,
    string MiddleName,
    string LastName,
    string IIN,
    string Email,
    string Phone,
    string EmergencyPhone,
    string Address,
    DateTime DateOfBirth,
    BloodType? BloodType,
    MaritalStatus? MaritalStatus);
    
public record UpdatePatientRequest(
    Guid Id,
    string FirstName,
    string MiddleName,
    string LastName,
    string IIN,
    string Email,
    string Phone,
    string EmergencyPhone,
    string Address,
    DateTime DateOfBirth,
    BloodType? BloodType,
    MaritalStatus? MaritalStatus);