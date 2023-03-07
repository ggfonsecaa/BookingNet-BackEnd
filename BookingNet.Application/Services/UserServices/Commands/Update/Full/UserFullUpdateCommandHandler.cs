﻿using AutoMapper;

using BookingNet.Application.Common.ServerCache;
using BookingNet.Application.Models.UserModels;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.UserServices.Commands.Update.Full
{
    public class UserFullUpdateCommandHandler : IRequestHandler<UserFullUpdateCommand, ErrorOr<UserModel>>
    {
        private readonly IServerCacheServiceStorage<User> _serverCacheService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserFullUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IServerCacheServiceStorage<User> serverCacheService)
        {
            _serverCacheService = serverCacheService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<UserModel>> Handle(UserFullUpdateCommand request, CancellationToken cancellationToken)
        {

            if (_unitOfWork?.UserRepository?.GetById(request.Id) is not User user)
            {
                throw new UserNotFoundException();
            }

            try
            {
                user.UpdateUserInfo(user, request.UserName, request.UserEmail);
                var userUpdated = _unitOfWork?.UserRepository.Update(user);
                await _unitOfWork?.SaveChangesAsync();
                await _serverCacheService.RemoveDataFromCacheAsync("[GET]_https://localhost:7204/api/v1/users");

                return _mapper.Map<UserModel>(userUpdated);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error durante el proceso de actualización de usuario");
            }
        }
    }
}