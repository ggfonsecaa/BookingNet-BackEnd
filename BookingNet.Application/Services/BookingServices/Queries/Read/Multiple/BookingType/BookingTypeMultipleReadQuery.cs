using BookingNet.Application.Models.BookingModels;
using BookingNet.Application.Models.NotificationModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.BookingServices.Queries.Read.Multiple.BookingTypeTypeQuery
{
    public class BookingTypeMultipleReadQuery : IRequest<ErrorOr<IEnumerable<BookingTypeModel>>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public BookingTypeMultipleReadQuery() { }
        public BookingTypeMultipleReadQuery(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}