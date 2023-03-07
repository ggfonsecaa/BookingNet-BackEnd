using BookingNet.Application.Models.UserModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.UserServices.Queries.Read.Single
{
    public class UserSingleReadQuery : IRequest<ErrorOr<UserModel>>
    {
        public int Id { get; set; }

        public UserSingleReadQuery(int id)
        {
            Id = id;
        }
    }
}