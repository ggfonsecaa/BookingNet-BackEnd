using BookingNet.Application.Models.GroupModels;
using BookingNet.Application.Models.UserModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.GroupServices.Queries.Read.Single
{
    public class GroupSingleReadQuery : IRequest<ErrorOr<GroupModel>>
    {
        public int Id { get; set; }

        public GroupSingleReadQuery(int id)
        {
            Id = id;
        }
    }
}