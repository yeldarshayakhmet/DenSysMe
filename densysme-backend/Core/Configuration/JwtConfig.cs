namespace Core.Configuration;

public record JwtConfig(string Secret, int AccessTokenExpiration, int RefreshTokenExpiration)
{
    public JwtConfig() : this(string.Empty, default, default) {}
}