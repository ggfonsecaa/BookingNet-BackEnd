using BookingNet.Application.Models.GroupModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.GroupServices.Queries.Search
{
    public class GroupSearchQuery : IRequest<ErrorOr<IEnumerable<GroupModel>>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}