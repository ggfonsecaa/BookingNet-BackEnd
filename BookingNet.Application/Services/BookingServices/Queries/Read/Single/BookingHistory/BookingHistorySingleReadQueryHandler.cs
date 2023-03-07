using AutoMapper;

using BookingNet.Application.Models.BookingModels;
using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Aggregates.BookingFlowAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.BookingServices.Queries.Read.Single.BookingHistory
{
    public class BookingHistorySingleReadQueryHandler : IRequestHandler<BookingHistorySingleReadQuery, ErrorOr<IEnumerable<BookingHistoryModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingHistorySingleReadQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<IEnumerable<BookingHistoryModel>>> Handle(BookingHistorySingleReadQuery request, CancellationToken cancellationToken)
        {

            if ((await _unitOfWork?.BookingRepository?.GetByQueryAsync(filter: x => x.Id == request.Id, includeProperties: "Flows,Flows.Flow,Flows.Flow.User,Flows.Flow.BookingStatus")) is not IEnumerable<Booking> bookingHistory)
            {
                throw new UserNotFoundException();
            }

            var history = bookingHistory
                .Select(x => new
                {
                    BookingStatus = x.Flows.Select(x => new BookingsFlows()
                    {
                        Id = x.Flow.Bookings.Select(x => x.Id).FirstOrDefault(),
                        BookingId = x.Flow.Bookings.Select(x => x.BookingId).FirstOrDefault(),
                        Booking = x.Flow.Bookings.Select(x => x.Booking).FirstOrDefault(),
                        FlowId = x.Flow.Bookings.Select(x => x.FlowId).FirstOrDefault(),
                        Flow = x.Flow.Bookings.Select(x => x.Flow).FirstOrDefault(),
                        DateStartFlow = x.Flow.Bookings.Select(x => x.DateStartFlow).FirstOrDefault(),
                        DateEndFlow = x.Flow.Bookings.Select(x => x.DateEndFlow).FirstOrDefault()
                    }).AsEnumerable()
                }).FirstOrDefault();

            return _mapper.Map<IEnumerable<BookingHistoryModel>>(history.BookingStatus).ToList();
        }

    }
}