using FluentValidation;

namespace BookingNet.Application.Services.GroupServices.Commands.Delete
{
    public class GroupDeleteCommandValidator : AbstractValidator<GroupDeleteCommand>
    {
        public GroupDeleteCommandValidator()
        {

        }
    }
}