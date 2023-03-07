using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.BookingModels;
using BookingNet.Application.Models.FlowsModels;
using BookingNet.Application.Models.RoleModels;
using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Aggregates.FlowAggregate;
using BookingNet.Domain.Aggregates.RoleAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Http;

using System.Security.Claims;

namespace BookingNet.Application.Services.BookingServices.Queries.Read.Multiple.BookingQuery
{
    public class BookingMultipleReadQueryHandler : IRequestHandler<BookingMultipleReadQuery, ErrorOr<IEnumerable<BookingModel>>>
    {
        private readonly IServerCacheServiceStorage<Booking> _serverCacheService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingMultipleReadQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, 
            IServerCacheServiceStorage<Booking> serverCacheService, IHttpContextAccessor contextAccessor)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<ErrorOr<IEnumerable<BookingModel>>> Handle(BookingMultipleReadQuery request, CancellationToken cancellationToken)
        {
            var cacheData = await _serverCacheService.GetDataListFromCacheAsync();

            var identity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userId = 0;
            var urlOrigin = _contextAccessor.HttpContext.Request.Path.ToString();

            if (identity != null)
            {
                userId = int.Parse(identity.FindFirst("Id").Value);
            }

            if (urlOrigin.Contains("manage"))
            {
                if (cacheData == null)
                {
                    if (await _unitOfWork?.BookingRepository?.GetByQueryAsync(filter: x => x.Flows.All(x => x.Flow.UserId == userId && x.Flow.BookingStatusId <= 5), includeProperties: "BookingType,User,Flows,Flows.Flow") is not IEnumerable<Booking> bookings)
                    {
                        throw new UserNotFoundException();
                    }

                    await _serverCacheService.SetDataListToCacheAsync(bookings);

                    return _mapper.Map<IEnumerable<BookingModel>>(bookings).ToList();
                }
                else
                {

                    return _mapper.Map<IEnumerable<BookingModel>>(cacheData).ToList();
                }
            }
            else 
            {
                if (cacheData == null)
                {
                    if (await _unitOfWork?.BookingRepository?.GetByQueryAsync(filter: x => x.UserId == userId, includeProperties: "BookingType,User,Flows,Flows.Flow") is not IEnumerable<Booking> bookings)
                    {
                        throw new UserNotFoundException();
                    }

                    await _serverCacheService.SetDataListToCacheAsync(bookings);

                    return _mapper.Map<IEnumerable<BookingModel>>(bookings).ToList();
                }
                else
                {

                    return _mapper.Map<IEnumerable<BookingModel>>(cacheData).ToList();
                }
            }

        }
    }
}