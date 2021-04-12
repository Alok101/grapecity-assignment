using OAuth.Data.Contract.Data;
using OAuth.Data.Contract.Interface;
using OAuth.Data.Contract.Models;
using System.Threading.Tasks;

namespace OAuth.Data.Service.Service
{
    public class UserDataService : IUserDataService
    {
        private readonly userContext _userContext;
        public UserDataService(userContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<long> CreateUser(User user)
        {
            _userContext.Set<User>().Add(user);
            await _userContext.SaveChangesAsync();
            return user.Id;
        }
    }
}
