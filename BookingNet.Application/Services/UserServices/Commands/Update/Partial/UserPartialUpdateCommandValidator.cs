using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingNet.Application.Services.UserServices.Commands.Update.Partial
{
    public class UserPartialUpdateCommandValidator : AbstractValidator<UserPartialUpdateCommand>
    {
        public UserPartialUpdateCommandValidator()
        {

        }
    }
}