using FluentValidation;

namespace BookingNet.Application.Services.UserServices.Commands.Create
{
    public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
    {
        public UserCreateCommandValidator()
        {
            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El campo '{ProperyName}' es requerido").NotEmpty().WithMessage("El campo '{PropertyName}' no debe estar vacío").WithName("Nombre de usuario")
                .MaximumLength(30).WithMessage("El campo '{PropertyName}' no debe tener más de {MaxLength} caracteres");

            RuleFor(x => x.UserEmail).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El campo '{ProperyName}' es requerido").NotEmpty().WithMessage("El campo '{PropertyName}' no debe estar vacío").WithName("Correo electrónico")
                .MaximumLength(50).WithMessage("El campo '{PropertyName} no debe tener más de {MaxLength}'")
                .EmailAddress().WithMessage("Debe ingresar una dirección de correo electrónico válida");
        }
    }
}