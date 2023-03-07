using BookingNet.Application.Models.GroupModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.GroupServices.Commands.Delete
{
    public class GroupDeleteCommand : IRequest<ErrorOr<GroupModel>>
    {
        public int Id { get; set; }

        public GroupDeleteCommand(int id) 
        { 
            Id = id;
        }
    }
}