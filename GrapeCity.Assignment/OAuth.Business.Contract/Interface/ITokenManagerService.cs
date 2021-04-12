using System.Collections.Generic;

namespace OAuth.Business.Contract.Interface
{
    public interface ITokenManagerService
    {
        public IDictionary<string, string> SetToken();
    }
}
