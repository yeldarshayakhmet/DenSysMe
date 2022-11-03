using Core.DataTransfer.User;

namespace Services.User;

public class LoginValidator : IRequestValidator<LoginDto>
{
    public Task<Dictionary<string, IEnumerable<string>>> Validate(LoginDto request)
    {
        var errors = new Dictionary<string, IEnumerable<string>>();
        if (string.IsNullOrWhiteSpace(request.IIN) || request.IIN.Length != 12)
        {
            errors.AddElement("IIN", "IIN is of incorrect format");
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            errors.AddElement("Password", "Password must not be empty");
        }

        return Task.FromResult(errors);
    }
}