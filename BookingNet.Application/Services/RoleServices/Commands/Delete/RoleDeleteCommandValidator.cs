using FluentValidation;

namespace BookingNet.Application.Services.RoleServices.Commands.Delete
{
    public class RoleDeleteCommandValidator : AbstractValidator<RoleDeleteCommand>
    {
        public RoleDeleteCommandValidator()
        {

        }
    }
}