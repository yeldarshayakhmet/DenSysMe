namespace Core.DataTransfer.User;

public record AuthResultDto(
    bool Successful,
    Guid? UserId = null,
    string? DisplayName = null,
    string? IIN = null,
    string? PhoneNumber = null,
    string? Email = null,
    NameDtoInt[]? Role = null,
    string? AccessToken = null,
    string? RefreshToken = null);