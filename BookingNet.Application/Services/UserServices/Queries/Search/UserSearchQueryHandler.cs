using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.DataFilters.UserFilters;
using BookingNet.Application.Models.UserModels;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;
using BookingNet.Domain.Interfaces.Filtering;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.UserServices.Queries.Search
{
    internal class UserSearchQueryHandler : IRequestHandler<UserSearchQuery, ErrorOr<IEnumerable<UserModel>>>
    {
        private readonly IQuerySpecification<User> _specificator;
        private readonly IServerCacheServiceStorage<User> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserSearchQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IQuerySpecification<User> specificator,
            IServerCacheServiceStorage<User> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _specificator = specificator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<IEnumerable<UserModel>>> Handle(UserSearchQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserName) && string.IsNullOrEmpty(request.UserEmail))
            {
                return _mapper.Map<IEnumerable<UserModel>>(Array.Empty<User>()).ToList();
            }

            var filtro = new UserDataFilterEvaluator(_specificator).GetCriteria(_mapper.Map<User>(request));
            var cacheData = await _serverCacheService.GetDataListFromCacheAsync();

            if (cacheData == null)
            {
                if (await _unitOfWork?.UserRepository?.GetByQueryAsync(filter: filtro, includeProperties: "Groups,Groups.Group,Groups.Group.Role,NotificationWay") is not IEnumerable<User> users || !users.Any())
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
