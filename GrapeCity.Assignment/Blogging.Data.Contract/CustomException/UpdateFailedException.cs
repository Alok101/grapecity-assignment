using System;

namespace Blogging.Data.Contract.CustomException
{
    public class UpdateFailedException : Exception
    {
        public UpdateFailedException(string message) : base(message)
        {
        }
    }
}
