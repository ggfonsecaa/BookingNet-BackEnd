using BookingNet.Application.Models.GroupModels;
using BookingNet.Application.Models.RoleModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.RoleServices.Queries.Read.Multiple
{
    public class RoleMultipleReadQuery : IRequest<ErrorOr<IEnumerable<RoleModel>>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public RoleMultipleReadQuery() { }
        public RoleMultipleReadQuery(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}