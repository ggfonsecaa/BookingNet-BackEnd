using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.GroupModels;
using BookingNet.Domain.Aggregates.GroupAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.GroupServices.Commands.Update.Full
{
    public class GroupFullUpdateCommandHandler : IRequestHandler<GroupFullUpdateCommand, ErrorOr<GroupModel>>
    {
        private readonly IServerCacheServiceStorage<Group> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupFullUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<Group> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<GroupModel>> Handle(GroupFullUpdateCommand request, CancellationToken cancellationToken)
        {

            if (_unitOfWork?.GroupRepository?.GetById(request.Id) is not Group group)
            {
                throw new UserNotFoundException();
            }

            try
            {
                group.UpdateGroupInfo(group, request.Name, request.RoleId);
                var groupUpdated = _unitOfWork?.GroupRepository.Update(group);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/groups");

                return _mapper.Map<GroupModel>(groupUpdated);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de actualización de usuario");
            }
        }
    }
}