using BookingNet.Application.Models.UserModels;
using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.UserServices.Queries.Search
{
    public class UserSearchQuery : IRequest<ErrorOr<IEnumerable<UserModel>>>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}