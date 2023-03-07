using BookingNet.Application.Models.GroupModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.GroupServices.Queries.Read.Multiple
{
    public class GroupMultipleReadQuery : IRequest<ErrorOr<IEnumerable<GroupModel>>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GroupMultipleReadQuery() { }
        public GroupMultipleReadQuery(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}