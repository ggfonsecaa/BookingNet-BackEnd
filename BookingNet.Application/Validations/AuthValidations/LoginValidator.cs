using BookingNet.Application.Contracts.AuthContracts;

using FluentValidation;

namespace BookingNet.Application.Validations.AuthValidations
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator() 
        {

            RuleFor(x => x.UserEmail).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El campo '{ProperyName}' es requerido").NotEmpty().WithMessage("El campo '{PropertyName}' no debe estar vacío").WithName("Correo electrónico")
                .MaximumLength(50).WithMessage("El campo '{PropertyName} no debe tener más de {MaxLength}'")
                .EmailAddress().WithMessage("Debe ingresar una dirección de correo electrónico válida");

            RuleFor(x => x.PassWord).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El campo '{ProperyName}' es requerido").NotEmpty().WithMessage("El campo '{PropertyName}' no debe estar vacío").WithName("Contraseña")
                .MaximumLength(20).WithMessage("El campo '{PropertyName}' no debe tener más de {MaxLength} caracteres")
                .MinimumLength(8).WithMessage("El campo '{PropertyName}' debe tener al menos {MinLength} caracteres");
        }
    }
}