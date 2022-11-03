namespace Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    public ICollection<AuthRole> AuthRoles { get; set; } = Array.Empty<AuthRole>();
}