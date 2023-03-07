using BookingNet.Application.Contracts.AuthContracts;
using BookingNet.Application.Models.AuthModels;
using FluentValidation.Results;

namespace BookingNet.Application.Interfaces
{
    public interface ILoginService
    {
        public Task<ValidationResult> Validate(LoginRequest request);
        public Task<LoginModel> LoginAsync(LoginRequest request);
    }
}