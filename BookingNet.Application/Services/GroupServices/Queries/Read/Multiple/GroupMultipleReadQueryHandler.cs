using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.GroupModels;
using BookingNet.Domain.Aggregates.GroupAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.GroupServices.Queries.Read.Multiple
{
    public class GroupMultipleReadQueryHandler : IRequestHandler<GroupMultipleReadQuery, ErrorOr<IEnumerable<GroupModel>>>
    {
        private readonly IServerCacheServiceStorage<Group> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupMultipleReadQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<Group> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<IEnumerable<GroupModel>>> Handle(GroupMultipleReadQuery request, CancellationToken cancellationToken)
        {
            var cacheData = await _serverCacheService.GetDataListFromCacheAsync();

            if (cacheData == null)
            {
                if (await _unitOfWork?.GroupRepository?.GetByQueryAsync(includeProperties: "Role") is not IEnumerable<Group> groups)
                {
                    throw new UserNotFoundException();
                }

                await _serverCacheService.SetDataListToCacheAsync(groups);

                return _mapper.Map<IEnumerable<GroupModel>>(groups).ToList();
            }
            else
            {

                return _mapper.Map<IEnumerable<GroupModel>>(cacheData).ToList();
            }
        }
    }
}