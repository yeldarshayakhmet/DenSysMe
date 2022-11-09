using Core.DataTransfer.User;
using Core.Entities;

namespace Services.User;

public interface IUserService
{
    Task<Guid> RegisterAsync(string password, IEnumerable<string> authRoles, CancellationToken cancellationToken = default);
    Task<Guid> RegisterAsync(string password, IEnumerable<AuthRole> authRoles, CancellationToken cancellationToken = default);
    Task<AuthResultDto> AuthenticateAsync<T>(LoginDto loginData) where T : Individual;
}