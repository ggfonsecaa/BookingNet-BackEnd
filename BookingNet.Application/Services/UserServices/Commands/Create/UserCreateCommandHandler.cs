using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.UserModels;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;
using BookingNet.Domain.Interfaces.Security;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.UserServices.Commands.Create
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, ErrorOr<UserModel>>
    {
        private readonly IServerCacheServiceStorage<User> _serverCacheService;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IHashGenerator _hashGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserCreateCommandHandler(IUnitOfWork unitOfWork, IPasswordGenerator passwordGenerator, IHashGenerator hashGenerator, 
            IMapper mapper, IServerCacheServiceStorage<User> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _passwordGenerator = passwordGenerator;
            _hashGenerator = hashGenerator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<UserModel>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            if (_unitOfWork?.UserRepository?.GetByQuery(filter: x => x.UserName == request.UserName || x.UserEmail == request.UserEmail).FirstOrDefault() is not null)
            {
                throw new DuplicateUserException();
            }

            var password = _hashGenerator.HashPassword(_passwordGenerator.CreateRandomPassword());
            User user = new(request.UserName, request.UserEmail, password, request.NotificationWayId);

            try
            {
                _unitOfWork?.UserRepository.Insert(user);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/users");

                return _mapper.Map<UserModel>(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de creación de usuario");
            }
        }
    }
}