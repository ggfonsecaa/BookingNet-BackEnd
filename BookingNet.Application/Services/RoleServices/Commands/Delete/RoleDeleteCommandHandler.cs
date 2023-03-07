using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.RoleModels;
using BookingNet.Domain.Aggregates.RoleAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.RoleServices.Commands.Delete
{
    public class RoleDeleteCommandHandler : IRequestHandler<RoleDeleteCommand, ErrorOr<RoleModel>>
    {
        private readonly IServerCacheServiceStorage<Role> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleDeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<Role> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<RoleModel>> Handle(RoleDeleteCommand request, CancellationToken cancellationToken)
        {
            if (_unitOfWork?.RoleRepository?.GetById(request.Id) is not Role role)
            {
                throw new UserNotFoundException();
            }

            try
            {
                _unitOfWork?.RoleRepository.Delete(role);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/roles");

                return _mapper.Map<RoleModel>(role);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de eliminación de rol");
            }
        }
    }
}