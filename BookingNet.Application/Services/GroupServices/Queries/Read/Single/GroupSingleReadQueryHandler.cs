using AutoMapper;

using BookingNet.Application.Models.GroupModels;
using BookingNet.Domain.Aggregates.GroupAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.GroupServices.Queries.Read.Single
{
    public class GroupSingleReadQueryHandler : IRequestHandler<GroupSingleReadQuery, ErrorOr<GroupModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupSingleReadQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<GroupModel>> Handle(GroupSingleReadQuery request, CancellationToken cancellationToken)
        {
            if ((await _unitOfWork?.GroupRepository?.GetByQueryAsync(filter: x => x.Id == request.Id, includeProperties: "Role")).FirstOrDefault() is not Group group)
            {
                throw new UserNotFoundException();
            }

            return _mapper.Map<GroupModel>(group);
        }
    }
}