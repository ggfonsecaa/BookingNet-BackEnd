using System.Net;

namespace BookingNet.Application.Contracts
{
    public class CustomResponse<T>
    {
        public HttpStatusCode Status => HttpStatusCode.OK;
        public string Message { get; set; }
        public T? Data { get; set; }

        public CustomResponse(string message, T? data)
        {
            Message = message;
            Data = data;
        }
    }
}