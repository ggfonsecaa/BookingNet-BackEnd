using BookingNet.Application.Models.GroupModels;
using BookingNet.Application.Models.RoleModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.RoleServices.Commands.Delete
{
    public class RoleDeleteCommand : IRequest<ErrorOr<RoleModel>>
    {
        public int Id { get; set; }

        public RoleDeleteCommand(int id) 
        { 
            Id = id;
        }
    }
}