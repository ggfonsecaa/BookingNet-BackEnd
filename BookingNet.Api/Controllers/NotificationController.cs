using AutoMapper;

using BookingNet.Application.Contracts;
using BookingNet.Application.Models.NotificationModels;
using BookingNet.Application.Services.NotificationServices.Queries.Read.Multiple.NotificationTypeQuery;
using BookingNet.Application.Services.NotificationServices.Queries.Read.Multiple.NotificationWayQuery;

using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BookingNet.Api.Controllers
{
    [EnableCors]
    [Authorize]
    [ApiController]
    //[ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60, NoStore = false)]
    [ApiVersion("1.0")]
    //[ValidateAntiForgeryToken]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/notifications")]
    public class NotificationController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public NotificationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("types")]
        [ProducesResponseType(typeof(CustomResponse<NotificationTypeModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotificationsTypes()
        {
            ErrorOr<IEnumerable<NotificationTypeModel>> authResult = await _mediator.Send(new NotificationTypeMultipleReadQuery());
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<IEnumerable<NotificationTypeModel>>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
        }

        [HttpGet("ways")]
        [ProducesResponseType(typeof(CustomResponse<NotificationWayModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotificationsWays()
        {
            ErrorOr<IEnumerable<NotificationWayModel>> authResult = await _mediator.Send(new NotificationWayMultipleReadQuery());
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<IEnumerable<NotificationWayModel>>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
        }
    }
}