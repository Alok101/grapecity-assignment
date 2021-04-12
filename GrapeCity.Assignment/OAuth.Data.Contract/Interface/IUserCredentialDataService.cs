using OAuth.Data.Contract.Models;
using System.Threading.Tasks;

namespace OAuth.Data.Contract.Interface
{
    public interface IUserCredentialDataService
    {
        public Task<long> CreateUserCredential(UserCredential userCred);
        public bool AuthenticateUserCredential(string userName, string password);
    }
}
