using AutoMapper;

using BookingNet.Application.Contracts.AuthContracts;
using BookingNet.Application.Models.AuthModels;
using BookingNet.Application.Models.UserModels;
using BookingNet.Domain.Aggregates.GroupAggregate;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Aggregates.UserGroupAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;
using BookingNet.Domain.Interfaces.Security;

using ErrorOr;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

namespace BookingNet.Application.Services.AuthServices.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<RegisterModel>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IValidator<RegisterRequest> _validator;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IHashGenerator _hashGenerator;
        private readonly IHashVerifier _hashVerifier;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IJwtTokenGenerator jwtTokenGenerator, IValidator<RegisterRequest> validator, IHashVerifier hashVerifier, IHashGenerator hashGenerator, IPasswordGenerator passwordGenerator)
        {
            _passwordGenerator = passwordGenerator;
            _jwtTokenGenerator = jwtTokenGenerator;
            _hashGenerator = hashGenerator;
            _hashVerifier = hashVerifier;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Validate(RegisterRequest request)
        {
            return await _validator.ValidateAsync(request);
        }

        public async Task<ErrorOr<RegisterModel>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (_unitOfWork?.UserRepository?.GetByQuery(filter: x => x.UserEmail == request.UserEmail || x.UserName == request.UserName).FirstOrDefault() is User user)
            {
                throw new Exception("El usuario ya se encuentra registrado");
            }

            if (_unitOfWork?.GroupRepository?.GetByQuery(filter: x => x.Role.Name == "Clientes").FirstOrDefault() is not Group group)
            {
                throw new Exception("No existe un grupo creado");
            }

            var passwordHash = _hashGenerator.HashPassword(request.PassWord) ?? _hashGenerator.HashPassword(_passwordGenerator.CreateRandomPassword());
            var userRequest = new User(request.UserName, request.UserEmail, passwordHash, 1);

            _unitOfWork.UserRepository.Insert(userRequest);
            await _unitOfWork.SaveChangesAsync();

            UsersGroups usersGroups = new()
            {
                Group = group,
                User = userRequest
            };

            userRequest.Groups.Add(usersGroups);

            _unitOfWork.UserRepository.Update(userRequest);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<RegisterModel>(request);
        }
    }
}