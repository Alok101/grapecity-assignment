using System;

namespace Blogging.Data.Contract.CustomException
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException(string message) : base(message)
        {
        }
    }
}
