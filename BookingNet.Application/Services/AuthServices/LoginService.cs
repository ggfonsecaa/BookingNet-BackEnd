using AutoMapper;

using BookingNet.Application.Contracts.AuthContracts;
using BookingNet.Application.Interfaces;
using BookingNet.Application.Models.AuthModels;
using BookingNet.Application.Models.GroupModels;
using BookingNet.Application.Models.UserModels;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Interfaces;
using BookingNet.Domain.Interfaces.Security;

using FluentValidation;
using FluentValidation.Results;

namespace BookingNet.Application.Services.Auth
{
    public class LoginService : ILoginService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IValidator<LoginRequest> _validator;
        private readonly IHashVerifier _hashVerifier;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LoginService(IValidator<LoginRequest> validator, IUnitOfWork unitOfWork, IMapper mapper, IHashVerifier hashVerifier, IJwtTokenGenerator jwtTokenGenerator)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hashVerifier = hashVerifier;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<ValidationResult> Validate(LoginRequest request)
        {
            return await _validator.ValidateAsync(request);
        }

        public async Task<LoginModel> LoginAsync(LoginRequest request)
        {
            if (_unitOfWork?.UserRepository?.GetByQuery(filter: x => x.UserEmail == request.UserEmail).FirstOrDefault() is not User user)
            {
                throw new Exception("El usuario o contraseña no son válidos");
            }

            if (!_hashVerifier.VerifyHashedPassword(user.PassWord, request.PassWord))
            {
                throw new Exception("El usuario o contraseña no son válidos");
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