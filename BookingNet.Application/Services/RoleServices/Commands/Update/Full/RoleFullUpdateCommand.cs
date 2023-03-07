using BookingNet.Application.Models.RoleModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.RoleServices.Commands.Update.Full
{
    public class RoleFullUpdateCommand : IRequest<ErrorOr<RoleModel>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}