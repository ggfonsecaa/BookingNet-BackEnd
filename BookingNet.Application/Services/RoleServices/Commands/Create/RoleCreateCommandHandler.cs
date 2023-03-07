using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.RoleModels;
using BookingNet.Domain.Aggregates.RoleAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.RoleServices.Commands.Create
{
    public class RoleCreateCommandHandler : IRequestHandler<RoleCreateCommand, ErrorOr<RoleModel>>
    {
        private readonly IServerCacheServiceStorage<Role> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<Role> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<RoleModel>> Handle(RoleCreateCommand request, CancellationToken cancellationToken)
        {
            if (_unitOfWork?.RoleRepository?.GetByQuery(filter: x => x.Name == request.Name).FirstOrDefault() is not null)
            {
                throw new DuplicateUserException();
            }

            Role role = new ( request.Name );

            try
            {
                _unitOfWork?.RoleRepository.Insert(role);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/roles");

                return _mapper.Map<RoleModel>(role);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de creación de rol");
            }
        }
    }
}