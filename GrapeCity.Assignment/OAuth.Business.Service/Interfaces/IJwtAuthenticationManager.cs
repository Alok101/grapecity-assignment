using OAuth.Business.Contract.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace OAuth.Business.Service.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        AuthenticationResponse Authenticate(string username, string password);
        IDictionary<string, string> UsersRefreshTokens { get; set; }
        AuthenticationResponse Authenticate(string username, Claim[] claims, string refreshToken);
    }
}
