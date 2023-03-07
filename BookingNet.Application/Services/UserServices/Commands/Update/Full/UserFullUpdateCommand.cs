using BookingNet.Application.Models.UserModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.UserServices.Commands.Update.Full
{
    public class UserFullUpdateCommand : IRequest<ErrorOr<UserModel>>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}