using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.GroupModels;
using BookingNet.Application.Models.UserModels;
using BookingNet.Domain.Aggregates.GroupAggregate;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.GroupServices.Commands.Delete
{
    public class GroupDeleteCommandHandler : IRequestHandler<GroupDeleteCommand, ErrorOr<GroupModel>>
    {
        private readonly IServerCacheServiceStorage<Group> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupDeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<Group> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<GroupModel>> Handle(GroupDeleteCommand request, CancellationToken cancellationToken)
        {
            if (_unitOfWork?.GroupRepository?.GetById(request.Id) is not Group group)
            {
                throw new UserNotFoundException();
            }

            try
            {
                _unitOfWork?.GroupRepository.Delete(group);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/groups");

                return _mapper.Map<GroupModel>(group);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de eliminación de grupo");
            }
        }
    }
}