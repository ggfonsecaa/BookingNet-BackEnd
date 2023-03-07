using System.Net;

namespace BookingNet.Domain.Exceptions.UserExceptions
{
    public class DuplicateUserException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
        public string ErrorMessage => "El nombre de usuario o correo electrónico ya se encuentran registrados";
    }
}