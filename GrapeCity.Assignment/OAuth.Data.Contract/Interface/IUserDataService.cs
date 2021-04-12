using OAuth.Data.Contract.Models;
using System.Threading.Tasks;

namespace OAuth.Data.Contract.Interface
{
    public interface IUserDataService
    {
        public Task<long> CreateUser(User user);
    }
}
