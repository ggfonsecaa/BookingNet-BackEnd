using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.GroupModels;
using BookingNet.Domain.Aggregates.GroupAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;
using BookingNet.Domain.Interfaces.Security;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.GroupServices.Commands.Create
{
    public class GroupCreateCommandHandler : IRequestHandler<GroupCreateCommand, ErrorOr<GroupModel>>
    {
        private readonly IServerCacheServiceStorage<Group> _serverCacheService;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IHashGenerator _hashGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupCreateCommandHandler(IUnitOfWork unitOfWork, IPasswordGenerator passwordGenerator, IHashGenerator hashGenerator, 
            IMapper mapper, IServerCacheServiceStorage<Group> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _passwordGenerator = passwordGenerator;
            _hashGenerator = hashGenerator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<GroupModel>> Handle(GroupCreateCommand request, CancellationToken cancellationToken)
        {
            if (_unitOfWork?.GroupRepository?.GetByQuery(filter: x => x.Name == request.Name).FirstOrDefault() is not null)
            {
                throw new DuplicateUserException();
            }

            Group group = new ( request.Name, request.RoleId );

            try
            {
                _unitOfWork?.GroupRepository.Insert(group);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/groups");

                return _mapper.Map<GroupModel>(group);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de creación de grupo");
            }
        }
    }
}