namespace Core.DataTransfer.User;

public record AuthResultDto(
    bool Successful,
    string ErrorMessage,
    Guid? UserId = null,
    string? DisplayName = null,
    NameDtoInt[]? Role = null,
    string? AccessToken = null,
    string? RefreshToken = null);