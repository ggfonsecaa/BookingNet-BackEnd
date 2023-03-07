using FluentValidation;

namespace BookingNet.Application.Services.UserServices.Commands.Delete
{
    public class UserDeleteCommandValidator : AbstractValidator<UserDeleteCommand>
    {
        public UserDeleteCommandValidator()
        {

        }
    }
}