using BookingNet.Application.Models.GroupModels;
using FluentValidation.Results;

using Microsoft.AspNetCore.JsonPatch;

namespace BookingNet.Application.Interfaces
{
    public interface IUserGroupService
    {
        public Task<ValidationResult> Validate(GroupModel request);
        public Task<IEnumerable<GroupModel>> GetAllAsync();
        public Task<IEnumerable<GroupModel>> GetByQueryAsync(GroupModel user);
        public Task<GroupModel> GetByIdAsync(int id);
        public Task<GroupModel> CreateAsync(GroupModel request);
        public Task<GroupModel> UpdateAsync(GroupModel request);
        public Task<GroupModel> UpdateAsync(int id, JsonPatchDocument<GroupModel> request);
        public Task<bool> DeleteAsync(int id);
    }
}