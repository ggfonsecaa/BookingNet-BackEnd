using BookingNet.Application.Models.AuthModels;
using BookingNet.Application.Models.BookingModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.AuthServices.Login
{
    public class LoginQuery : IRequest<ErrorOr<LoginModel>>
    {
        public string UserEmail { get; set; }
        public string PassWord { get; set; }
    }
}