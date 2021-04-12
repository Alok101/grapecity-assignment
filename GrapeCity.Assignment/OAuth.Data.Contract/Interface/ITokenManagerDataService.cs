using OAuth.Data.Contract.Models;

namespace OAuth.Data.Contract.Interface
{
    public interface ITokenManagerDataService
    {
        public TokenManager GetTokenKey();
    }
}
