using BookingNet.Application.Contracts.AuthContracts;
using BookingNet.Application.Models.AuthModels;
using FluentValidation.Results;

namespace BookingNet.Application.Interfaces
{
    public interface IRegisterService
    {
        public Task<ValidationResult> Validate(RegisterRequest request);
        public Task<RegisterModel> RegisterAsync(RegisterRequest request);
    }
}