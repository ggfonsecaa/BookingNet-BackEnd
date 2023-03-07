using BookingNet.Application.Models.FlowsModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.FlowServices.Queries.Read.Single
{
    public class FlowSingleReadQuery : IRequest<ErrorOr<FlowModel>>
    {
        public int Id { get; set; }

        public FlowSingleReadQuery(int id)
        {
            Id = id;
        }
    }
}