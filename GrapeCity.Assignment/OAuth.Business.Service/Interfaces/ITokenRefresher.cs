using OAuth.Business.Contract.Models;

namespace OAuth.Business.Service.Interfaces
{
    public interface ITokenRefresher
    {
        AuthenticationResponse Refresh(RefreshCred refreshCred);
    }
}
