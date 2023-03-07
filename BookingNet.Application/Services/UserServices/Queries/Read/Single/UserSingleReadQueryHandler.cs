using AutoMapper;

using BookingNet.Application.Models.UserModels;
using BookingNet.Domain.Aggregates.UserAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.UserServices.Queries.Read.Single
{
    public class SingleReadQueryHandler : IRequestHandler<UserSingleReadQuery, ErrorOr<UserModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SingleReadQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<UserModel>> Handle(UserSingleReadQuery request, CancellationToken cancellationToken)
        {

            if (_unitOfWork?.UserRepository?.GetById(request.Id) is not User user)
            {
                throw new UserNotFoundException();
            }

            return _mapper.Map<UserModel>(user);
        }
    }
}