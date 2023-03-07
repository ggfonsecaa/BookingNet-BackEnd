using BookingNet.Application.Models.UserModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.UserServices.Queries.Read.Multiple
{
    public class UserMultipleReadQuery : IRequest<ErrorOr<IEnumerable<UserModel>>>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public UserMultipleReadQuery() { }
        public UserMultipleReadQuery(int id, string userName, string email)
        {
            Id = id;
            UserName = userName;
            UserEmail = email;
        }
    }
}