using AutoMapper;

using BookingNet.Application.Models.RoleModels;
using BookingNet.Domain.Aggregates.RoleAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.RoleServices.Queries.Read.Single
{
    public class RoleSingleReadQueryHandler : IRequestHandler<RoleSingleReadQuery, ErrorOr<RoleModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleSingleReadQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<RoleModel>> Handle(RoleSingleReadQuery request, CancellationToken cancellationToken)
        {

            if (_unitOfWork?.RoleRepository?.GetById(request.Id) is not Role role)
            {
                throw new UserNotFoundException();
            }

            return _mapper.Map<RoleModel>(role);
        }
    }
}