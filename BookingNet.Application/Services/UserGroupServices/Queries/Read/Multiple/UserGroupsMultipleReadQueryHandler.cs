using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.UserModels;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;
using MediatR;

namespace BookingNet.Application.Services.UserGroupServices.Queries.Read.Multiple
{
    public class UserGroupsMultipleReadQueryHandler : IRequestHandler<UserGroupsMultipleReadQuery, ErrorOr<IEnumerable<UserModel>>>
    {
        private readonly IServerCacheServiceStorage<User> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserGroupsMultipleReadQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<User> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<IEnumerable<UserModel>>> Handle(UserGroupsMultipleReadQuery request, CancellationToken cancellationToken)
        {
            var cacheData = await _serverCacheService.GetDataListFromCacheAsync();

            if (cacheData == null)
            {
                if (await _unitOfWork?.UserRepository?.GetByQueryAsync(filter: x => x.Id == request.Id, includeProperties: "Groups,Groups.Group,Groups.Group.Role") is not IEnumerable<User> users)
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