using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.GroupModels;
using BookingNet.Domain.Aggregates.GroupAggregate;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.JsonPatch;

namespace BookingNet.Application.Services.GroupServices.Commands.Update.Partial
{
    public class GroupPartialUpdateCommandHandler : IRequestHandler<GroupPartialUpdateCommand, ErrorOr<GroupModel>>
    {
        private readonly IServerCacheServiceStorage<Group> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupPartialUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<Group> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<GroupModel>> Handle(GroupPartialUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patchEntity = _mapper.Map<JsonPatchDocument<Group>>(request.PatchDocument);
                var group = await _unitOfWork?.GroupRepository.UpdateAsync(request.Id, patchEntity);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/groups");

                return _mapper.Map<GroupModel>(group);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de actualización de grupo");
            }
        }
    }
}
