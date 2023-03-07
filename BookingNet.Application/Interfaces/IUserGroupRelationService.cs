using BookingNet.Application.Models.GroupModels;
using FluentValidation.Results;

using Microsoft.AspNetCore.JsonPatch;

namespace BookingNet.Application.Interfaces
{
    public interface IUserGroupRelationService
    {
        public Task<ValidationResult> ValidateUser(GroupModel request);
        public Task<IEnumerable<GroupModel>> GetAllUsersAsync(int idGroup);
        public Task<IEnumerable<GroupModel>> GetUserByQueryAsync(int idGroup, GroupModel user);
        public Task<GroupModel> GetUserByIdAsync(int idGroup, int idUser);
        public Task<GroupModel> CreateUserAsync(int idGroup, GroupModel request);
        public Task<GroupModel> UpdateUserAsync(int idGroup, GroupModel request);
        public Task<GroupModel> UpdateUserAsync(int idGroup, int idUser, JsonPatchDocument<GroupModel> request);
        public Task<bool> DeleteUserAsync(int idGroup, int idUser);

        public Task<ValidationResult> ValidateGroup(GroupModel request);
        public Task<IEnumerable<GroupModel>> GetAllGroupsAsync(int idUser);
        public Task<IEnumerable<GroupModel>> GetGroupByQueryAsync(int idUser, GroupModel user);
        public Task<GroupModel> GetGroupByIdAsync(int idUser, int idGroup);
        public Task<GroupModel> CreateGroupAsync(int idUser, GroupModel request);
        public Task<GroupModel> UpdateGroupAsync(int idUser, GroupModel request);
        public Task<GroupModel> UpdateGroupAsync(int idUser, int idGroup, JsonPatchDocument<GroupModel> request);
        public Task<bool> DeleteGroupAsync(int idUser, int idGroup);
    }
}