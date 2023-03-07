using AutoMapper;

using BookingNet.Application.Contracts.AuthContracts;
using BookingNet.Application.Models.AuthModels;
using BookingNet.Application.Models.UserModels;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;
using BookingNet.Domain.Interfaces.Security;

using ErrorOr;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

namespace BookingNet.Application.Services.AuthServices.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<LoginModel>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IValidator<LoginRequest> _validator;
        private readonly IHashVerifier _hashVerifier;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoginQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IJwtTokenGenerator jwtTokenGenerator, IValidator<LoginRequest> validator, IHashVerifier hashVerifier)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtTokenGenerator = jwtTokenGenerator;
            _validator = validator;
            _hashVerifier = hashVerifier;
        }

        public async Task<ValidationResult> Validate(LoginRequest request)
        {
            return await _validator.ValidateAsync(request);
        }

        public async Task<ErrorOr<LoginModel>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            if (_unitOfWork?.UserRepository?.GetByQuery(filter: x => x.UserEmail == request.UserEmail, includeProperties: "Groups,Groups.Group,Groups.Group.Role").FirstOrDefault() is not User user)
            {
                throw new UserNotFoundException();
            }

            if (!_hashVerifier.VerifyHashedPassword(user.PassWord, request.PassWord))
            {
                throw new UserNotFoundException();
            }

            var userModel = _mapper.Map<UserModel>(user);
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new LoginModel
            {
                User = userModel,
                Token = token
            };
        }
    }
}