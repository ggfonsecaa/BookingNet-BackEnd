using AutoMapper;

using BookingNet.Application.Models.BookingModels;
using BookingNet.Domain.Aggregates.BookingAggregate;
using BookingNet.Domain.Exceptions.UserExceptions;
using BookingNet.Domain.Interfaces;

using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Http;

using System.Security.Claims;

namespace BookingNet.Application.Services.BookingServices.Queries.Read.Single.BookingQuery
{
    public class BookingSingleReadQueryHandler : IRequestHandler<BookingSingleReadQuery, ErrorOr<BookingModel>>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingSingleReadQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BookingModel>> Handle(BookingSingleReadQuery request, CancellationToken cancellationToken)
        {
            var identity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userId = identity.FindFirst("Id").Value;
            }

            if (_unitOfWork?.BookingRepository?.GetById(request.Id) is not Booking booking)
            {
                throw new UserNotFoundException();
            }

            return _mapper.Map<BookingModel>(booking);
        }
    }
}