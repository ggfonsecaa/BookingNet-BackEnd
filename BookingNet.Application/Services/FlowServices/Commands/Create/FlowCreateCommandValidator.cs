using FluentValidation;

namespace BookingNet.Application.Services.FlowServices.Commands.Create
{
    public class FlowCreateCommandValidator : AbstractValidator<FlowCreateCommand>
    {
        public FlowCreateCommandValidator()
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El campo '{ProperyName}' es requerido").NotEmpty().WithMessage("El campo '{PropertyName}' no debe estar vacío").WithName("Nombre de usuario")
                .MaximumLength(30).WithMessage("El campo '{PropertyName}' no debe tener más de {MaxLength} caracteres");
        }
    }
}