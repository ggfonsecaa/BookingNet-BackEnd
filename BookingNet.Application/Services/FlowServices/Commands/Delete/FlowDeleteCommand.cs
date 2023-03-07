using BookingNet.Application.Models.FlowsModels;
using BookingNet.Application.Models.GroupModels;
using BookingNet.Application.Models.RoleModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.FlowServices.Commands.Delete
{
    public class FlowDeleteCommand : IRequest<ErrorOr<FlowModel>>
    {
        public int Id { get; set; }

        public FlowDeleteCommand(int id) 
        { 
            Id = id;
        }
    }
}