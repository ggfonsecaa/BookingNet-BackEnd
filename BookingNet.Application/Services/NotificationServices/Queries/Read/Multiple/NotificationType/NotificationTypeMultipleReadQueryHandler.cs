using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.NotificationModels;
using BookingNet.Domain.Aggregates.NotificationAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.NotificationServices.Queries.Read.Multiple.NotificationTypeQuery
{
    public class NotificationTypeMultipleReadQueryHandler : IRequestHandler<NotificationTypeMultipleReadQuery, ErrorOr<IEnumerable<NotificationTypeModel>>>
    {
        private readonly IServerCacheServiceStorage<NotificationType> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationTypeMultipleReadQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<NotificationType> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<IEnumerable<NotificationTypeModel>>> Handle(NotificationTypeMultipleReadQuery request, CancellationToken cancellationToken)
        {
            var cacheData = await _serverCacheService.GetDataListFromCacheAsync();

            if (cacheData == null)
            {
                if (await _unitOfWork?.NotificationTypeRepository?.GetAllAsync() is not IEnumerable<NotificationType> notificationsTypes)
                {
                    throw new UserNotFoundException();
                }

                await _serverCacheService.SetDataListToCacheAsync(notificationsTypes);

                return _mapper.Map<IEnumerable<NotificationTypeModel>>(notificationsTypes).ToList();
            }
            else
            {

                return _mapper.Map<IEnumerable<NotificationTypeModel>>(cacheData).ToList();
            }
        }
    }
}