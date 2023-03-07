using BookingNet.Application.Models.FlowsModels;
using BookingNet.Application.Models.RoleModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.FlowServices.Commands.Update.Full
{
    public class FlowFullUpdateCommand : IRequest<ErrorOr<FlowModel>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool HasPreviousFlow { get; set; }
        public int FlowId { get; set; }
        public int BookingStatusId { get; set; }
    }
}