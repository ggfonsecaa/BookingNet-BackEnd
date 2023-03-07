using BookingNet.Application.Models.RoleModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.RoleServices.Commands.Create
{
    public class RoleCreateCommand : IRequest<ErrorOr<RoleModel>>
    {
        public string Name { get; set; }
    }
}