using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.BookingModels;
using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Aggregates.BookingFlowAggregate;
using BookingNet.Domain.Aggregates.FlowAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.BookingServices.Commands.Create
{
    public class BookingCreateCommandHandler : IRequestHandler<BookingCreateCommand, ErrorOr<BookingModel>>
    {
        private readonly IServerCacheServiceStorage<Booking> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<Booking> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BookingModel>> Handle(BookingCreateCommand request, CancellationToken cancellationToken)
        {
            if (_unitOfWork?.BookingRepository?.GetByQuery(filter: x => x.BookingDate == request.BookingDate).FirstOrDefault() is not null)
            {
                throw new DuplicateUserException();
            }

            if ((await _unitOfWork?.FlowRepository?.GetByQueryAsync(filter: x => x.HasPreviousFlow == false && x.Name != "Rechazo del evento")).FirstOrDefault() is not Flow flow)
            {
                throw new DuplicateUserException();
            }

            Booking booking = new ( request.BookingDate, request.Attendants, 0, request.BookingTypeId, request.UserId, request.Comments );

            try
            {
                BookingsFlows bookingsFlows = new()
                {
                    FlowId = flow.Id,
                    Booking = booking,
                    DateStartFlow = DateTime.Now
                };

                booking.Flows.Add(bookingsFlows);

                _unitOfWork?.BookingRepository.Insert(booking);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/bookings");

                return _mapper.Map<BookingModel>(booking);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de creación de la reserva");
            }
        }
    }
}