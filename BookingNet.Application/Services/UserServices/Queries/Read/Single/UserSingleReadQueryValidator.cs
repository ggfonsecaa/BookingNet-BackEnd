using FluentValidation;

namespace BookingNet.Application.Services.UserServices.Queries.Read.Single
{
    public class SingleReadQueryValidator : AbstractValidator<UserSingleReadQuery>
    {
        public SingleReadQueryValidator()
        {

        }
    }
}