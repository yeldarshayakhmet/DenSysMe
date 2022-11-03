using Core.DataTransfer.User;
using Core.Entities;

namespace Services.User;

public interface IUserService
{
    Task<Guid> Register(string password, IReadOnlyCollection<string> authRoles, CancellationToken cancellationToken);
    Task<Guid> Register(string password, IReadOnlyCollection<AuthRole> authRoles, CancellationToken cancellationToken);
    Task<AuthResultDto> Authenticate<T>(LoginDto loginData) where T : Individual;
}