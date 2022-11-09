using Microsoft.AspNetCore.Authorization;

namespace API;

public class AuthorizeData : IAuthorizeData
{
    public AuthorizeData(string? policy = null, string? roles = null, string? authenticationSchemes = null)
    {
        Policy = policy;
        Roles = roles;
        AuthenticationSchemes = authenticationSchemes;
    }
    
    public string? Policy { get; set; }
    public string? Roles { get; set; }
    public string? AuthenticationSchemes { get; set; }
}
