using BookingNet.Application.Models.FlowsModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.FlowServices.Commands.Create
{
    public class FlowCreateCommand : IRequest<ErrorOr<FlowModel>>
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool HasPreviousFlow { get; set; }
        public int FlowId { get; set; }
        public int BookingStatusId { get; set; }
    }
}