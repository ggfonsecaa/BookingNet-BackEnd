using System.Net;

namespace BookingNet.Domain.Exceptions
{
    public interface IServiceException
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }
    }
}