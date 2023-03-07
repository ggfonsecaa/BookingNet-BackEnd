using AutoMapper;

using BookingNet.Application.Models.FlowsModels;
using BookingNet.Application.Models.RoleModels;
using BookingNet.Domain.Aggregates.FlowAggregate;
using BookingNet.Domain.Aggregates.RoleAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

namespace BookingNet.Application.Services.FlowServices.Queries.Read.Single
{
    public class FlowSingleReadQueryHandler : IRequestHandler<FlowSingleReadQuery, ErrorOr<FlowModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FlowSingleReadQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<FlowModel>> Handle(FlowSingleReadQuery request, CancellationToken cancellationToken)
        {

            if (_unitOfWork?.FlowRepository?.GetById(request.Id) is not Flow flow)
            {
                throw new UserNotFoundException();
            }

            return _mapper.Map<FlowModel>(flow);
        }
    }
}