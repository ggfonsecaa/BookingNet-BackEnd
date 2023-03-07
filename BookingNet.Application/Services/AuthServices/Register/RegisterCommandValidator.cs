using FluentValidation;

namespace BookingNet.Application.Services.AuthServices.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.UserEmail).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El campo '{ProperyName}' es requerido")
                .NotEmpty().WithMessage("El campo '{PropertyName}' no debe estar vacío")
                .MaximumLength(30).WithMessage("El campo '{ProperyName}' no debe tener más de {MaxLength} caracteres")
                .WithName("Nombre de usuario");

            RuleFor(x => x.UserEmail).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El campo '{ProperyName}' es requerido")
                .NotEmpty().WithMessage("El campo '{PropertyName}' no debe estar vacío")
                .EmailAddress().WithMessage("Debe ingresar una dirección de correo electrónico válida")
                .MaximumLength(50).WithMessage("El campo '{ProperyName}' no debe tener más de {MaxLength} caracteres")
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