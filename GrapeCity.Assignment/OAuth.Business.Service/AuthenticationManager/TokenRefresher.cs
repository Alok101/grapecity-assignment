using Microsoft.IdentityModel.Tokens;
using OAuth.Business.Contract.Models;
using OAuth.Business.Service.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace OAuth.Business.Service.AuthenticationManager
{
    public class TokenRefresher : ITokenRefresher
    {
        private readonly byte[] refreshKey;
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public TokenRefresher(byte[] refreshKey, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.refreshKey = refreshKey;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
        public AuthenticationResponse Refresh(RefreshCred refreshCred)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(refreshCred.AccessToken,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(refreshKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out validatedToken);
            var jwtToken = validatedToken as JwtSecurityToken;
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token passed!");
            }
            var userName = principal.Identity.Name;
            //if (refreshCred.RefreshToken != jwtAuthenticationManager.UsersRefreshTokens[userName])
            //{
            //    throw new SecurityTokenException("Invalid token passed!");
            //}
            return jwtAuthenticationManager.Authenticate(userName, principal.Claims.ToArray(), refreshCred.RefreshToken);
        }
    }
}

