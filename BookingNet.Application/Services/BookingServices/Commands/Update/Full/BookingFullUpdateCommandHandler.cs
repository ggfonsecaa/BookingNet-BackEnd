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

using Microsoft.AspNetCore.Http;

namespace BookingNet.Application.Services.BookingServices.Commands.Update.Full
{
    public class BookingFullUpdateCommandHandler : IRequestHandler<BookingFullUpdateCommand, ErrorOr<BookingModel>>
    {
        private readonly IServerCacheServiceStorage<Booking> _serverCacheService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingFullUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<Booking> serverCacheService, IHttpContextAccessor contextAccessor)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ErrorOr<BookingModel>> Handle(BookingFullUpdateCommand request, CancellationToken cancellationToken)
        {

            if ((await _unitOfWork?.BookingRepository?.GetByQueryAsync(filter: x => x.Id == request.Id, includeProperties: "Flows,Flows.Flow", asTracking: true)).FirstOrDefault() is not Booking booking)
            {
                throw new UserNotFoundException();
            }

            var lastFlowId = booking.Flows.OrderByDescending(x => x.DateStartFlow).Select(x => x.Flow.Id).FirstOrDefault();

            var urlOrigin = _contextAccessor.HttpContext.Request.Path.ToString();
            Flow flow;

            if (urlOrigin.Contains("aprove"))
            {
                flow = (await _unitOfWork?.FlowRepository?.GetByQueryAsync(filter: x => x.HasPreviousFlow == true && x.FlowId == lastFlowId)).FirstOrDefault();

                if (flow is null)
                {
                    throw new DuplicateUserException();
                }
            }
            else if (urlOrigin.Contains("decline"))
            {
                flow = (await _unitOfWork?.FlowRepository?.GetByQueryAsync(filter: x => x.HasPreviousFlow == false && x.Name == "Rechazo del evento")).FirstOrDefault();

                if (flow is null)
                {
                    throw new DuplicateUserException();
                }
            }
            else 
            {
                flow = (await _unitOfWork?.FlowRepository?.GetByQueryAsync(filter: x => x.Id == lastFlowId)).FirstOrDefault();

                if (flow is null)
                {
                    throw new DuplicateUserException();
                }
            }


            try
            {
                if (urlOrigin.Contains("decline") || urlOrigin.Contains("aprove")) 
                { 
                    booking.UpdateBookingInfo(request.Price, request.Comments);
                    booking.Flows.OrderByDescending(x => x.DateStartFlow).FirstOrDefault().StartFlow();

                    BookingsFlows bookingsFlows = new()
                    {
                        Flow = flow,
                        Booking = booking,
                        DateStartFlow = DateTime.Now
                    };
                    booking.Flows.Add(bookingsFlows);
                }
                else 
                {
                    booking.ChangeBookingInfo(request.BookingDate, request.Attendants, request.BookingTypeId, request.Comments);
                }
                


                _unitOfWork?.BookingRepository.Update(booking);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/bookings");

                return _mapper.Map<BookingModel>(booking);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de actualización del flujo");
            }
        }
    }
}