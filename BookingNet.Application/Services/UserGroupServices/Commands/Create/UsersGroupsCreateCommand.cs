using BookingNet.Application.Models.UserGroupModel;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.UserGroupServices.Commands.Create
{
    public class UsersGroupsCreateCommand : IRequest<ErrorOr<UsersGroupsAsociationModel>>
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public UsersGroupsCreateCommand() { }

        public UsersGroupsCreateCommand(int idUser, UsersGroupsAsociationModel request) 
        { 
            UserId = idUser;
            GroupId = request.GroupId;
        }
    }
}