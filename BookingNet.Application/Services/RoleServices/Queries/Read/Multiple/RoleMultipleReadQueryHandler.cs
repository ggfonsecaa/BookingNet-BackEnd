using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.RoleModels;
using BookingNet.Domain.Aggregates.RoleAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.RoleServices.Queries.Read.Multiple
{
    public class RoleMultipleReadQueryHandler : IRequestHandler<RoleMultipleReadQuery, ErrorOr<IEnumerable<RoleModel>>>
    {
        private readonly IServerCacheServiceStorage<Role> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleMultipleReadQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<Role> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<IEnumerable<RoleModel>>> Handle(RoleMultipleReadQuery request, CancellationToken cancellationToken)
        {
            var cacheData = await _serverCacheService.GetDataListFromCacheAsync();

            if (cacheData == null)
            {
                if (await _unitOfWork?.RoleRepository?.GetAllAsync() is not IEnumerable<Role> roles)
                {
                    throw new UserNotFoundException();
                }

                await _serverCacheService.SetDataListToCacheAsync(roles);

                return _mapper.Map<IEnumerable<RoleModel>>(roles).ToList();
            }
            else
            {

                return _mapper.Map<IEnumerable<RoleModel>>(cacheData).ToList();
            }
        }
    }
}