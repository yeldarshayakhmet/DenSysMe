namespace Core.Entities;

public class AuthRole
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<User> Users { get; set; } = Array.Empty<User>();
}