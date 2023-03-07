using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.FlowsModels;
using BookingNet.Application.Models.RoleModels;
using BookingNet.Domain.Aggregates.FlowAggregate;
using BookingNet.Domain.Aggregates.RoleAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.FlowServices.Queries.Read.Multiple
{
    public class FlowMultipleReadQueryHandler : IRequestHandler<FlowMultipleReadQuery, ErrorOr<IEnumerable<FlowModel>>>
    {
        private readonly IServerCacheServiceStorage<Flow> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FlowMultipleReadQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<Flow> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<IEnumerable<FlowModel>>> Handle(FlowMultipleReadQuery request, CancellationToken cancellationToken)
        {
            var cacheData = await _serverCacheService.GetDataListFromCacheAsync();

            if (cacheData == null)
            {
                if (await _unitOfWork?.FlowRepository?.GetAllAsync() is not IEnumerable<Flow> flows)
                {
                    throw new UserNotFoundException();
                }

                await _serverCacheService.SetDataListToCacheAsync(flows);

                return _mapper.Map<IEnumerable<FlowModel>>(flows).ToList();
            }
            else
            {

                return _mapper.Map<IEnumerable<FlowModel>>(cacheData).ToList();
            }
        }
    }
}