using BookingNet.Application.Models.AuthModels;
using BookingNet.Application.Models.BookingModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.AuthServices.Register
{
    public class RegisterCommand : IRequest<ErrorOr<RegisterModel>>
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PassWord { get; set; }
    }
}