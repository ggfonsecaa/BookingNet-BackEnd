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

namespace BookingNet.Application.Services.FlowServices.Commands.Create
{
    public class FlowCreateCommandHandler : IRequestHandler<FlowCreateCommand, ErrorOr<FlowModel>>
    {
        private readonly IServerCacheServiceStorage<Flow> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FlowCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<Flow> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<FlowModel>> Handle(FlowCreateCommand request, CancellationToken cancellationToken)
        {
            if (_unitOfWork?.FlowRepository?.GetByQuery(filter: x => x.Name == request.Name).FirstOrDefault() is not null)
            {
                throw new DuplicateUserException();
            }

            Flow flow = new ( request.Name, request.UserId, request.BookingStatusId , request.HasPreviousFlow, request.FlowId );

            try
            {
                _unitOfWork?.FlowRepository.Insert(flow);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/flows");

                return _mapper.Map<FlowModel>(flow);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de creación del flujo");
            }
        }
    }
}