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
    public UserDto(Individual person) : this(
        person.IIN,
        person.FirstName,
        person.LastName,
        person.Email,
        person.PhoneNumber,
        person.UserId.HasValue ? person.User!.Id : null,
        person.UserId.HasValue ? person.User!.PasswordHash : null,
        person.UserId.HasValue ? person.User!.PasswordSalt : null,
        person.UserId.HasValue ? person.User?.AuthRoles?.Select(role => new NameDtoInt(role.Id, role.Name)).ToArray() : null) {}
};