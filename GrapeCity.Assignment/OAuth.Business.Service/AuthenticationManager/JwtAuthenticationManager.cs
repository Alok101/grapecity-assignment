using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OAuth.Business.Contract.Interface;
using OAuth.Business.Contract.Models;
using OAuth.Business.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OAuth.Business.Service.AuthenticationManager
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        public IDictionary<string, string> UsersRefreshTokens { get; set; }
        private readonly string key;
        private readonly IRefreshTokenGenerator refreshTokenGenerator;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public JwtAuthenticationManager(string key, IRefreshTokenGenerator refreshTokenGenerator, IUserService userService, IConfiguration configuration)
        {
            this.key = key;
            this.refreshTokenGenerator = refreshTokenGenerator;
            UsersRefreshTokens = new Dictionary<string, string>();
            _userService = userService;
            _configuration = configuration;
        }
        public AuthenticationResponse Authenticate(string username, string password)
        {
            if (!_userService.ValidUser(new UserLoginModel { UserName = username, Password = CrptoPasswordHash.EncodePasswordToBase64(password) }))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var access_tokenKey = Encoding.ASCII.GetBytes(_configuration.GetSection("accessKey").Value);
            var refresh_tokenKey = Encoding.ASCII.GetBytes(key);
            return new AuthenticationResponse
            {
                AccessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(GenerateDiscriptor(access_tokenKey, username, true))),
                RefreshToken = tokenHandler.WriteToken(tokenHandler.CreateToken(GenerateDiscriptor(refresh_tokenKey, username, false)))
            };
        }
        public AuthenticationResponse Authenticate(string username, Claim[] claims, string refreshToken)
        {
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                    )
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var accessToken = refreshTokenGenerator.GenerateToken();
            return new AuthenticationResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        }
        private SecurityTokenDescriptor GenerateDiscriptor(byte[] tokenKey, string username, bool isAccessToken)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(isAccessToken ? 10 : 12),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature
                    )
            };
            return tokenDescriptor;
        }

    }
}
