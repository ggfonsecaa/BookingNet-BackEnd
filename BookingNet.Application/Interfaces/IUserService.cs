using BookingNet.Application.Models.UserModels;
using FluentValidation.Results;

using Microsoft.AspNetCore.JsonPatch;

namespace BookingNet.Application.Interfaces
{
    public interface IUserService
    {
        public Task<ValidationResult> Validate(UserModel request);
        public Task<IEnumerable<UserModel>> GetAllAsync();
        public Task<IEnumerable<UserModel>> GetByQueryAsync(UserModel user);
        public Task<IEnumerable<UserModel>> GetByIdAsync(int id);
        public Task<UserModel> CreateAsync(UserModel request);
        public Task<UserModel> UpdateAsync(UserModel request);
        public Task<UserModel> UpdateAsync(int id, JsonPatchDocument<UserModel> request);
        public Task<bool> DeleteAsync(int id);
    }
}