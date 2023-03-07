using BookingNet.Application.Models.UserModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.UserServices.Commands.Delete
{
    public class UserDeleteCommand : IRequest<ErrorOr<UserModel>>
    {
        public int Id { get; set; }

        public UserDeleteCommand(int id) 
        { 
            Id = id;
        }
    }
}