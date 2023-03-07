using BookingNet.Application.Models.BookingModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.BookingServices.Commands.Create
{
    public class BookingCreateCommand : IRequest<ErrorOr<BookingModel>>
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public int Attendants { get; set; }
        public string? Comments { get; set; }
        public int BookingTypeId { get; set; }
        public int UserId { get; set; }
    }
}