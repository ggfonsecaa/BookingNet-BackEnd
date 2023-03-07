using BookingNet.Application.Models.UserModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.UserServices.Commands.Create
{
    public class UserCreateCommand : IRequest<ErrorOr<UserModel>>
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int NotificationWayId { get; set; }
    }
}