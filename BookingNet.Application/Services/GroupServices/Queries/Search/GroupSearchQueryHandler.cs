using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.GroupModels;
using BookingNet.Domain.Aggregates.GroupAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;
using BookingNet.Domain.Interfaces.Filtering;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.GroupServices.Queries.Search
{
    internal class GroupSearchQueryHandler : IRequestHandler<GroupSearchQuery, ErrorOr<IEnumerable<GroupModel>>>
    {
        private readonly IQuerySpecification<Group> _specificator;
        private readonly IServerCacheServiceStorage<Group> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupSearchQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IQuerySpecification<Group> specificator,
            IServerCacheServiceStorage<Group> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _specificator = specificator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<IEnumerable<GroupModel>>> Handle(GroupSearchQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return _mapper.Map<IEnumerable<GroupModel>>(Array.Empty<Group>()).ToList();
            }

            //var filtro = new UserDataFilterEvaluator(_specificator).GetCriteria(_mapper.Map<Group>(request));
            var cacheData = await _serverCacheService.GetDataListFromCacheAsync();

            if (cacheData == null)
            {
                if (await _unitOfWork?.UserRepository?.GetByQueryAsync(/*filter: filtro*/) is not IEnumerable<Group> groups || !groups.Any())
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
