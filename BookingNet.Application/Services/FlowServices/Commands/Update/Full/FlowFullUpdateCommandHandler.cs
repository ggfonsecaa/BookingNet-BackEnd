using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.FlowsModels;
using BookingNet.Domain.Aggregates.FlowAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.FlowServices.Commands.Update.Full
{
    public class FlowFullUpdateCommandHandler : IRequestHandler<FlowFullUpdateCommand, ErrorOr<FlowModel>>
    {
        private readonly IServerCacheServiceStorage<Flow> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FlowFullUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<Flow> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<FlowModel>> Handle(FlowFullUpdateCommand request, CancellationToken cancellationToken)
        {

            if (_unitOfWork?.FlowRepository?.GetById(request.Id) is not Flow flow)
            {
                throw new UserNotFoundException();
            }

            try
            {
                flow.UpdateFlowInfo(request.Name, request.HasPreviousFlow, request.FlowId, request.UserId);
                var flowUpdated = _unitOfWork?.FlowRepository.Update(flow);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/flows");

                return _mapper.Map<FlowModel>(flowUpdated);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de actualización del flujo");
            }
        }
    }
}