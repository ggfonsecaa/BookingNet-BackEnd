using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.UserModels;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.JsonPatch;

namespace BookingNet.Application.Services.UserServices.Commands.Update.Partial
{
    public class UserPartialUpdateCommandHandler : IRequestHandler<UserPartialUpdateCommand, ErrorOr<UserModel>>
    {
        private readonly IServerCacheServiceStorage<User> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserPartialUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<User> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<UserModel>> Handle(UserPartialUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patchEntity = _mapper.Map<JsonPatchDocument<User>>(request.PatchDocument);
                var user = await _unitOfWork?.UserRepository.UpdateAsync(request.Id, patchEntity);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/users");

                return _mapper.Map<UserModel>(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de actualización de usuario");
            }
        }
    }
}
