using FluentValidation;

namespace BookingNet.Application.Services.BookingServices.Commands.Update.Full
{
    public class BookingFullUpdateCommandValidator : AbstractValidator<BookingFullUpdateCommand>
    {
        public BookingFullUpdateCommandValidator()
        {
            RuleFor(x => x.BookingDate).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El campo '{ProperyName}' es requerido").NotEmpty().WithMessage("El campo '{PropertyName}' no debe estar vacío").WithName("Fecha de reserva");

            RuleFor(x => x.Attendants).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El campo '{ProperyName}' es requerido").NotEmpty().WithMessage("El campo '{PropertyName}' no debe estar vacío").WithName("Asistentes")
                .LessThan(32767).WithMessage("El campo '{ProperyName}' debe ser menor a {MaxLength}").GreaterThan(0).WithMessage("El campo '{ProperyName}' debe ser mayor a {MaxLength}");

            RuleFor(x => x.Comments).Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("El campo '{ProperyName}' es requerido").NotEmpty().WithMessage("El campo '{PropertyName}' no debe estar vacío").WithName("Comentarios")
                .MaximumLength(255).WithMessage("El campo '{PropertyName}' no debe tener más de {MaxLength} caracteres");
        }
    }
}