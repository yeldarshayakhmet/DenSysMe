using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Core.Configuration;
using Core.DataTransfer;
using Core.DataTransfer.User;
using Core.Entities;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Services.User;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtConfig _tokenConfig;

    public UserService(IUnitOfWork unitOfWork, IOptionsSnapshot<JwtConfig> tokenConfig)
    {
        _unitOfWork = unitOfWork;
        _tokenConfig = tokenConfig.Value;
    }

    public async Task<Guid> RegisterAsync(string password, IReadOnlyCollection<string> authRoles, CancellationToken cancellationToken = default)
    {
        var dbRoles = await _unitOfWork.Collection<AuthRole>()
            .Where(role => authRoles.Contains(role.Name))
            .ToListAsync(cancellationToken);
        
        if (authRoles.Count == dbRoles.Count)
            return await RegisterAsync(password, dbRoles, cancellationToken);
        
        throw new ApplicationException("Invalid authorization roles set upon creating a user");
    }

    public async Task<Guid> RegisterAsync(string password, IReadOnlyCollection<AuthRole> authRoles, CancellationToken cancellationToken = default)
    {
        var user = new Core.Entities.User();
        CreateEncryptedPassword(password, out var hash, out var salt);
        user.PasswordHash = hash;
        user.PasswordSalt = salt;
        user.AuthRoles = authRoles.ToList();
        var newUser = _unitOfWork.Create(user);
        await _unitOfWork.SaveAsync(cancellationToken);
        return newUser.Id;
    }

    public async Task<AuthResultDto> AuthenticateAsync<T>(LoginDto loginData) where T : Individual
    {
        Expression<Func<UserDto, bool>> userFilter = person => person.IIN == loginData.IIN && person.UserId.HasValue;
        var user = typeof(T) == typeof(Patient)
            ? await _unitOfWork.Collection<Patient>()
                .Select(patient => new UserDto(patient))
                .AsNoTracking()
                .FirstOrDefaultAsync(userFilter)
            : await GetEmployeeByFilter(userFilter);
        
        if (user is null)
            return new AuthResultDto(false, "Invalid username or password");
        
        if (!IsPasswordValid(loginData.Password, user.PasswordHash!, user.PasswordSalt!))
            return new AuthResultDto(false, "Invalid username or password");
        
        var accessToken = CreateToken(user, _tokenConfig.AccessTokenExpiration);
        var refreshToken = CreateToken(user, _tokenConfig.RefreshTokenExpiration);
        return new AuthResultDto(
            true,
            string.Empty,
            user.UserId!.Value,
            $"{user.FirstName} {user.LastName}",
            user.Roles!.Select(role => new NameDtoInt(role.Id, role.Name)).ToArray(),
            accessToken,
            refreshToken);
    }

    private static void CreateEncryptedPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private static bool IsPasswordValid(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }

    private string CreateToken(UserDto user, int expirationInMinutes)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserId!.Value.ToString()),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(nameof(user.IIN), user.IIN),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.MobilePhone, user.Phone)
        };
        claims.AddRange(user.Roles!.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddMinutes(expirationInMinutes), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<UserDto?> GetEmployeeByFilter(Expression<Func<UserDto, bool>> filter) => await _unitOfWork
        .Collection<Core.Entities.Doctor>()
        .Select(doctor => new UserDto(doctor))
        .Concat(_unitOfWork
            .Collection<Manager>()
            .Select(manager => new UserDto(manager)))
        .FirstOrDefaultAsync(filter);
}