using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.BookingModels;
using BookingNet.Application.Models.NotificationModels;
using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Aggregates.NotificationAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.BookingServices.Queries.Read.Multiple.BookingTypeTypeQuery
{
    public class BookingTypeMultipleReadQueryHandler : IRequestHandler<BookingTypeMultipleReadQuery, ErrorOr<IEnumerable<BookingTypeModel>>>
    {
        private readonly IServerCacheServiceStorage<BookingType> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingTypeMultipleReadQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<BookingType> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<IEnumerable<BookingTypeModel>>> Handle(BookingTypeMultipleReadQuery request, CancellationToken cancellationToken)
        {
            var cacheData = await _serverCacheService.GetDataListFromCacheAsync();

            if (cacheData == null)
            {
                if (await _unitOfWork?.BookingTypeRepository?.GetAllAsync() is not IEnumerable<BookingType> bookingsTypes)
                {
                    throw new UserNotFoundException();
                }

                await _serverCacheService.SetDataListToCacheAsync(bookingsTypes);

                return _mapper.Map<IEnumerable<BookingTypeModel>>(bookingsTypes).ToList();
            }
            else
            {

                return _mapper.Map<IEnumerable<BookingTypeModel>>(cacheData).ToList();
            }
        }
    }
}