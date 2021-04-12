namespace Blogging.Data.Contract.Models
{
    public class SystemResponse
    {
        public ResponseType Response { get; set; }
        public string Message { get; set; }
        public SystemResponse()
        {
        }
        public static SystemResponse SuccessResponse(string message)
        {
            return new SystemResponse { Response = ResponseType.success, Message = message };
        }
    }
    public enum ResponseType
    {
        error = 0,
        success = 1
    }
}
