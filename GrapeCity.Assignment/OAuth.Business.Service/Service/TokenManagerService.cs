using OAuth.Business.Contract.Interface;
using OAuth.Data.Contract.Interface;
using System.Collections.Generic;

namespace OAuth.Business.Service.Service
{
    public class TokenManagerService : ITokenManagerService
    {
        private readonly ITokenManagerDataService _tokenManager;
        private IDictionary<string, string> securityKey { get; set; }
        public TokenManagerService(ITokenManagerDataService tokenManager)
        {
            _tokenManager = tokenManager;
        }
        public IDictionary<string, string> SetToken()
        {
            var token = _tokenManager.GetTokenKey();
            return securityKey = new Dictionary<string, string> {
                { "refresh_key", token.RefreshTokenSecret }, { "access_key", token.AccessTokenSecret }
            };
        }
    }
}
