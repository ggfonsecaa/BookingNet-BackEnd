using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.UserGroupModel;
using BookingNet.Application.Models.UserModels;
using BookingNet.Domain.Aggregates.GroupAggregate;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Aggregates.UserGroupAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;
using BookingNet.Domain.Interfaces.Security;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.UserGroupServices.Commands.Create
{
    public class UsersGroupsCreateCommandHandler : IRequestHandler<UsersGroupsCreateCommand, ErrorOr<UsersGroupsAsociationModel>>
    {
        private readonly IServerCacheServiceStorage<User> _serverCacheService;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IHashGenerator _hashGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersGroupsCreateCommandHandler(IUnitOfWork unitOfWork, IPasswordGenerator passwordGenerator, IHashGenerator hashGenerator, 
            IMapper mapper, IServerCacheServiceStorage<User> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _passwordGenerator = passwordGenerator;
            _hashGenerator = hashGenerator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<UsersGroupsAsociationModel>> Handle(UsersGroupsCreateCommand request, CancellationToken cancellationToken)
        {
            if (_unitOfWork?.UserRepository?.GetById(request.UserId) is not User user)
            {
                throw new DuplicateUserException();
            }

            if (_unitOfWork?.GroupRepository?.GetById(request.GroupId) is not Group group)
            {
                throw new DuplicateUserException();
            }

            UsersGroups usersGroups = new UsersGroups();
            usersGroups.User = user;
            usersGroups.Group = group;

            user.Groups.Add(usersGroups);

            try
            {
                _unitOfWork?.UserRepository.Update(user);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/users");

                return _mapper.Map<UsersGroupsAsociationModel>(new UsersGroupsAsociationModel());
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de creación de usuario");
            }
        }
    }
}