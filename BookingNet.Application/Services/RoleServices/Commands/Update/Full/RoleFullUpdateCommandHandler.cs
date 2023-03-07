using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.RoleModels;
using BookingNet.Domain.Aggregates.RoleAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.RoleServices.Commands.Update.Full
{
    public class RoleFullUpdateCommandHandler : IRequestHandler<RoleFullUpdateCommand, ErrorOr<RoleModel>>
    {
        private readonly IServerCacheServiceStorage<Role> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleFullUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<Role> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<RoleModel>> Handle(RoleFullUpdateCommand request, CancellationToken cancellationToken)
        {

            if (_unitOfWork?.RoleRepository?.GetById(request.Id) is not Role role)
            {
                throw new UserNotFoundException();
            }

            try
            {
                role.UpdateRoleInfo(role, request.Name);
                var roleUpdated = _unitOfWork?.RoleRepository.Update(role);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/roles");

                return _mapper.Map<RoleModel>(roleUpdated);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de actualización de rol");
            }
        }
    }
}