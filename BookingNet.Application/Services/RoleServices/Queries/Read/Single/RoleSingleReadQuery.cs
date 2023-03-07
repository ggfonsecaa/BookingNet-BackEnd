using BookingNet.Application.Models.RoleModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.RoleServices.Queries.Read.Single
{
    public class RoleSingleReadQuery : IRequest<ErrorOr<RoleModel>>
    {
        public int Id { get; set; }

        public RoleSingleReadQuery(int id)
        {
            Id = id;
        }
    }
}