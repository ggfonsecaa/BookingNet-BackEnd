using AutoMapper;

using BookingNet.Application.Contracts.AuthContracts;
using BookingNet.Application.Interfaces;
using BookingNet.Application.Models.AuthModels;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Interfaces;
using BookingNet.Domain.Interfaces.Security;

using FluentValidation;
using FluentValidation.Results;

namespace BookingNet.Application.Services.Auth
{
    public class RegisterService : IRegisterService
    {
        private readonly IValidator<RegisterRequest> _validator;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IHashGenerator _hashGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterService(IValidator<RegisterRequest> validator, IUnitOfWork unitOfWork, IMapper mapper, IHashGenerator hashGenerator, IPasswordGenerator passwordGenerator)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hashGenerator = hashGenerator;
            _passwordGenerator = passwordGenerator;
        }


        public async Task<ValidationResult> Validate(RegisterRequest request)
        {
            return await _validator.ValidateAsync(request);
        }
        public async Task<RegisterModel> RegisterAsync(RegisterRequest request)
        {
            if (_unitOfWork?.UserRepository?.GetByQuery(filter: x => x.UserEmail == request.UserEmail || x.UserName == request.UserName).FirstOrDefault() is User user)
            {
                throw new Exception("El usuario ya se encuentra registrado");
            }

            var passwordHash = _hashGenerator.HashPassword(request.PassWord) ?? _hashGenerator.HashPassword(_passwordGenerator.CreateRandomPassword());
            var userRequest = new User(request.UserName, request.UserEmail, passwordHash, 1);
            _unitOfWork.UserRepository.Insert(userRequest);

            await _unitOfWork.SaveChangesAsync();

            var model = _mapper.Map<RegisterModel>(request);
            return model;
        }
    }
}