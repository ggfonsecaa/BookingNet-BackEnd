using FluentValidation;

namespace BookingNet.Application.Services.AuthServices.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.UserEmail).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El campo '{ProperyName}' es requerido")
                .NotEmpty().WithMessage("El campo '{PropertyName}' no debe estar vacío")
                .EmailAddress().WithMessage("Debe ingresar una dirección de correo electrónico válida")
                .WithName("Correo electrónico");

            RuleFor(x => x.PassWord).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El campo '{ProperyName}' es requerido")
                .NotEmpty().WithMessage("El campo '{PropertyName}' no debe estar vacío")
                .MinimumLength(8).WithMessage("El campo '{ProperyName}' dete tener al menos {MinLength} caracteres")
                .MaximumLength(20).WithMessage("El campo '{ProperyName}' no debe tener más de {MaxLength} caracteres")
                .WithName("Contraseña");
        }
    }
}