using System.Net;

namespace BookingNet.Domain.Exceptions.UserExceptions
{
    public class UserNotFoundException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
        public string ErrorMessage => "El usuario o la contraseña no son válidos";
    }
}