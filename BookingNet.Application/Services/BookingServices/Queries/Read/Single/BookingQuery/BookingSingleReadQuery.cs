using BookingNet.Application.Models.BookingModels;
using BookingNet.Application.Models.FlowsModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.BookingServices.Queries.Read.Single.BookingQuery
{
    public class BookingSingleReadQuery : IRequest<ErrorOr<BookingModel>>
    {
        public int Id { get; set; }

        public BookingSingleReadQuery(int id)
        {
            Id = id;
        }
    }
}