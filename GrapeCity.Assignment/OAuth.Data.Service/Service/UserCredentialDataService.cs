using OAuth.Data.Contract.Data;
using OAuth.Data.Contract.Interface;
using OAuth.Data.Contract.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OAuth.Data.Service.Service
{
    public class UserCredentialDataService : IUserCredentialDataService
    {
        private readonly userContext _userContext;
        public UserCredentialDataService(userContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<long> CreateUserCredential(UserCredential userCred)
        {
            _userContext.Set<UserCredential>().Add(userCred);
            await _userContext.SaveChangesAsync();
            return userCred.Id;
        }
        public bool AuthenticateUserCredential(string userName, string password)
        {
            var user = _userContext.Set<UserCredential>().SingleOrDefault(x => x.UserName == userName && x.Password == password);
            return user == null ? true : false;
        }
    }
}
