using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
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

    public async Task<Guid> Register(string password, IReadOnlyCollection<string> authRoles, CancellationToken cancellationToken)
    {
        var dbRoles = await _unitOfWork.Collection<AuthRole>()
            .Where(role => authRoles.Contains(role.Name))
            .ToListAsync(cancellationToken);
        
        if (authRoles.Count == dbRoles.Count)
            return await Register(password, dbRoles, cancellationToken);
        
        throw new ApplicationException("Invalid authorization roles set upon creating a user");
    }

    public async Task<Guid> Register(string password, IReadOnlyCollection<AuthRole> authRoles, CancellationToken cancellationToken)
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

    public async Task<AuthResultDto> Authenticate<T>(LoginDto loginData) where T : Individual
    {
        Individual? person = typeof(T) == typeof(Patient)
            ? await _unitOfWork.Collection<Patient>()
                .Include(p => p.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.IIN == loginData.IIN)
            : await GetEmployeeByIIN(loginData.IIN);
        
        if (person?.UserId is null)
            throw new AuthenticationException("User was not found");

        var user = person.User!;
        if (!IsPasswordValid(loginData.Password, user.PasswordHash, user.PasswordSalt))
            return new AuthResultDto(false);
        
        var accessToken = CreateToken(person, user, _tokenConfig.AccessTokenExpiration);
        var refreshToken = CreateToken(person, user, _tokenConfig.RefreshTokenExpiration);
        return new AuthResultDto(
            true,
            person.UserId.Value,
            $"{person.FirstName} {person.LastName}",
            person.IIN,
            person.PhoneNumber,
            person.Email,
            user.AuthRoles.Select(role => new NameDtoInt(role.Id, role.Name)).ToArray(),
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

    private string CreateToken(Individual person, Core.Entities.User user, int expirationInMinutes)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, $"{person.FirstName} {person.LastName}"),
            new(nameof(person.IIN), person.IIN),
            new(ClaimTypes.Email, person.Email)
        };
        claims.AddRange(user.AuthRoles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddMinutes(expirationInMinutes), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<Employee?> GetEmployeeByIIN(string iin)
    {
        var manager = await _unitOfWork.Collection<Manager>()
            .Include(m => m.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.IIN == iin);
        return manager is null
            ? await _unitOfWork.Collection<Doctor>()
                .Include(d => d.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.IIN == iin)
            : manager;
    }
}