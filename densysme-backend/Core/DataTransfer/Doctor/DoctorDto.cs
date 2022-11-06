namespace Core.DataTransfer.Doctor;

public record DoctorDto(string FirstName, string LastName, string Phone, byte[]? Photo);