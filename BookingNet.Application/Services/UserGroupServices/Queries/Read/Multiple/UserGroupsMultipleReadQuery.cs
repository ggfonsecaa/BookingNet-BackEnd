using BookingNet.Application.Models.UserModels;

using ErrorOr;
using MediatR;

namespace BookingNet.Application.Services.UserGroupServices.Queries.Read.Multiple
{
    public class UserGroupsMultipleReadQuery : IRequest<ErrorOr<IEnumerable<UserModel>>>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public ICollection<UsersGroupsModel> UserGroups { get; set; }

        public UserGroupsMultipleReadQuery() { }
        public UserGroupsMultipleReadQuery(int id = 0, string userName = "", string email = "")
        {
            Id = id;
            UserName = userName;
            UserEmail = email;
        }
    }
}
