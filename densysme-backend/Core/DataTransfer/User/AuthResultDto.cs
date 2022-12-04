namespace Core.DataTransfer.User;

public record AuthResultDto(
    bool Successful,
    string ErrorMessage,
    UserResultDto? User = null,
    string? AccessToken = null,
    string? RefreshToken = null);
    
public record UserResultDto(Guid UserId, string DisplayName, NameDtoInt[] Roles);