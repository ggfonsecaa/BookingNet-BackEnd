using BookingNet.Application.Models.FlowsModels;
using BookingNet.Application.Models.GroupModels;
using BookingNet.Application.Models.RoleModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.FlowServices.Queries.Read.Multiple
{
    public class FlowMultipleReadQuery : IRequest<ErrorOr<IEnumerable<FlowModel>>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public FlowMultipleReadQuery() { }
        public FlowMultipleReadQuery(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}