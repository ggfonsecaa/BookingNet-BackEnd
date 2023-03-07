using BookingNet.Application.Models.GroupModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.GroupServices.Commands.Update.Full
{
    public class GroupFullUpdateCommand : IRequest<ErrorOr<GroupModel>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
    }
}