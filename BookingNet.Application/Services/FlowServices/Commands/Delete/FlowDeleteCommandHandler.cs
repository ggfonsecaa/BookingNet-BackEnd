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

namespace BookingNet.Application.Services.FlowServices.Commands.Delete
{
    public class FlowDeleteCommandHandler : IRequestHandler<FlowDeleteCommand, ErrorOr<FlowModel>>
    {
        private readonly IServerCacheServiceStorage<Flow> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FlowDeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<Flow> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<FlowModel>> Handle(FlowDeleteCommand request, CancellationToken cancellationToken)
        {
            if (_unitOfWork?.FlowRepository?.GetById(request.Id) is not Flow flow)
            {
                throw new UserNotFoundException();
            }

            try
            {
                _unitOfWork?.FlowRepository.Delete(flow);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/flows");

                return _mapper.Map<FlowModel>(flow);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de eliminación del flujo");
            }
        }
    }
}