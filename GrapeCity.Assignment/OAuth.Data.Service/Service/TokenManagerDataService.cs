using Microsoft.EntityFrameworkCore;
using OAuth.Data.Contract.Data;
using OAuth.Data.Contract.Interface;
using OAuth.Data.Contract.Models;
using System.Linq;

namespace OAuth.Data.Service.Service
{
    public class TokenManagerDataService : ITokenManagerDataService
    {
        private readonly userContext _userContext;
        public TokenManagerDataService(userContext userContext)
        {
            _userContext = userContext;
        }
        public TokenManager GetTokenKey()
        {
            return _userContext.Set<TokenManager>()
                  .AsNoTracking()
                  .FirstOrDefault();
        }
    }
}
