using BookingNet.Application.Models.BookingModels;
using BookingNet.Application.Models.FlowsModels;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.BookingServices.Queries.Read.Single.BookingHistory
{
    public class BookingHistorySingleReadQuery : IRequest<ErrorOr<IEnumerable<BookingHistoryModel>>>
    {
        public int Id { get; set; }

        public BookingHistorySingleReadQuery(int id)
        {
            Id = id;
        }
    }
}