using FluentValidation;

namespace BookingNet.Application.Services.RoleServices.Commands.Update.Full
{
    public class RoleFullUpdateCommandValidator : AbstractValidator<RoleFullUpdateCommand>
    {
        public RoleFullUpdateCommandValidator()
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El campo '{ProperyName}' es requerido").NotEmpty().WithMessage("El campo '{PropertyName}' no debe estar vacío").WithName("Nombre de usuario")
                .MaximumLength(30).WithMessage("El campo '{PropertyName}' no debe tener más de {MaxLength} caracteres");
        }
    }
}