using BookingNet.Application.Models.GroupModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.GroupServices.Commands.Create
{
    public class GroupCreateCommand : IRequest<ErrorOr<GroupModel>>
    {
        public string Name { get; set; }
        public int RoleId { get; set; }
    }
}