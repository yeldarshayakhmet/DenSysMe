using Core.Entities;

namespace Core.DataTransfer.User;

public record UserDto(
    string IIN,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    Guid? UserId,
    byte[]? PasswordHash,
    byte[]? PasswordSalt,
    NameDtoInt[]? Roles)
{
    public UserDto(Individual employee) : this(
        employee.IIN,
        employee.FirstName,
        employee.LastName,
        employee.Email,
        employee.PhoneNumber,
        employee.UserId.HasValue ? employee.User!.Id : null,
        employee.UserId.HasValue ? employee.User!.PasswordHash : null,
        employee.UserId.HasValue ? employee.User!.PasswordSalt : null,
        employee.UserId.HasValue ? employee.User?.AuthRoles.Select(role => new NameDtoInt(role.Id, role.Name)).ToArray() : null) {}
};