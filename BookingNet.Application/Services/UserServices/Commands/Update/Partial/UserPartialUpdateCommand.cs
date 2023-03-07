using BookingNet.Application.Models.UserModels;

using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.JsonPatch;

namespace BookingNet.Application.Services.UserServices.Commands.Update.Partial
{
    public class UserPartialUpdateCommand : IRequest<ErrorOr<UserModel>>
    {
        public int Id { get; set; }
        public JsonPatchDocument<UserModel> PatchDocument { get; set; }

        public UserPartialUpdateCommand(int id, JsonPatchDocument<UserModel> patchDocument)
        {
            Id = id;
            PatchDocument = patchDocument;
        }
    }
}