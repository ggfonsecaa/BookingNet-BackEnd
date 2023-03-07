using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.UserModels;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;
using ErrorOr;
using MediatR;

namespace BookingNet.Application.Services.UserServices.Queries.Read.Multiple
{
    public class MultipleReadQueryHandler : IRequestHandler<UserMultipleReadQuery, ErrorOr<IEnumerable<UserModel>>>
    {
        private readonly IServerCacheServiceStorage<User> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MultipleReadQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<User> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<IEnumerable<UserModel>>> Handle(UserMultipleReadQuery request, CancellationToken cancellationToken)
        {
            var cacheData = await _serverCacheService.GetDataListFromCacheAsync();

            if (cacheData == null)
            {
                if (_unitOfWork?.UserRepository?.GetAll() is not IEnumerable<User> users)
                {
                    throw new UserNotFoundException();
                }

                await _serverCacheService.SetDataListToCacheAsync(users);

                return _mapper.Map<IEnumerable<UserModel>>(users).ToList();
            }
            else
            {

                return _mapper.Map<IEnumerable<UserModel>>(cacheData).ToList();
            }
        }
    }
}