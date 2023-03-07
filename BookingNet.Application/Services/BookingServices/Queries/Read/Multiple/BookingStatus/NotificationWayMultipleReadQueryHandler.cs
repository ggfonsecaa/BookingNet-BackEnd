using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.NotificationModels;
using BookingNet.Domain.Aggregates.NotificationAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.BookingServices.Queries.Read.Multiple.BookingStatusQuery
{
    public class NotificationWayMultipleReadQueryHandler : IRequestHandler<NotificationWayMultipleReadQuery, ErrorOr<IEnumerable<NotificationWayModel>>>
    {
        private readonly IServerCacheServiceStorage<NotificationWay> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationWayMultipleReadQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<NotificationWay> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<IEnumerable<NotificationWayModel>>> Handle(NotificationWayMultipleReadQuery request, CancellationToken cancellationToken)
        {
            var cacheData = await _serverCacheService.GetDataListFromCacheAsync();

            if (cacheData == null)
            {
                if (await _unitOfWork?.NotificationWayRepository?.GetAllAsync() is not IEnumerable<NotificationWay> notificationsWays)
                {
                    throw new UserNotFoundException();
                }

                await _serverCacheService.SetDataListToCacheAsync(notificationsWays);

                return _mapper.Map<IEnumerable<NotificationWayModel>>(notificationsWays).ToList();
            }
            else
            {

                return _mapper.Map<IEnumerable<NotificationWayModel>>(cacheData).ToList();
            }
        }
    }
}