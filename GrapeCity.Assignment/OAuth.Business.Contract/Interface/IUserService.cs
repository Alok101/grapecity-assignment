using OAuth.Business.Contract.Models;
using System.Threading.Tasks;

namespace OAuth.Business.Contract.Interface
{
    public interface IUserService
    {
        public Task<string> AddNewUser(UserViewModel userModel);
        public bool ValidUser(UserLoginModel login);
    }
}
