using BookingNet.Application.Models.GroupModels;

using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.JsonPatch;

namespace BookingNet.Application.Services.GroupServices.Commands.Update.Partial
{
    public class GroupPartialUpdateCommand : IRequest<ErrorOr<GroupModel>>
    {
        public int Id { get; set; }
        public JsonPatchDocument<GroupModel> PatchDocument { get; set; }

        public GroupPartialUpdateCommand(int id, JsonPatchDocument<GroupModel> patchDocument)
        {
            Id = id;
            PatchDocument = patchDocument;
        }
    }
}