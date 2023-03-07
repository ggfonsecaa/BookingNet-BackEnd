using AutoMapper;
using BookingNet.Application.Contracts;
using BookingNet.Application.Models.BookingModels;
using BookingNet.Application.Models.FlowsModels;
using BookingNet.Application.Models.NotificationModels;
using BookingNet.Application.Services.BookingServices.Commands.Create;
using BookingNet.Application.Services.BookingServices.Commands.Update.Full;
using BookingNet.Application.Services.BookingServices.Queries.Read.Multiple.BookingQuery;
using BookingNet.Application.Services.BookingServices.Queries.Read.Multiple.BookingTypeTypeQuery;
using BookingNet.Application.Services.BookingServices.Queries.Read.Single;
using BookingNet.Application.Services.BookingServices.Queries.Read.Single.BookingHistory;
using BookingNet.Application.Services.BookingServices.Queries.Read.Single.BookingQuery;
using BookingNet.Application.Services.FlowServices.Commands.Create;
using BookingNet.Application.Services.FlowServices.Queries.Read.Multiple;
using BookingNet.Application.Services.FlowServices.Queries.Read.Single;
using BookingNet.Application.Services.NotificationServices.Queries.Read.Multiple.NotificationTypeQuery;
using BookingNet.Application.Services.NotificationServices.Queries.Read.Multiple.NotificationWayQuery;
using ErrorOr;
using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using System.ComponentModel.DataAnnotations;

namespace BookingNet.Api.Controllers
{
    [EnableCors]
    [Authorize]
    [ApiController]
    //[ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60, NoStore = false)]
    [ApiVersion("1.0")]
    //[ValidateAntiForgeryToken]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/bookings")]
    public class BookingController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BookingController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{idBooking?}")]
        [ProducesResponseType(typeof(CustomResponse<BookingModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookings([FromRoute] int idFlow)
        {
            if (idFlow == 0)
            {
                ErrorOr<IEnumerable<BookingModel>> authResult = await _mediator.Send(new BookingMultipleReadQuery());
                return authResult.Match(authResult => CustomOkResponse(_mapper.Map<IEnumerable<BookingModel>>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
            }
            else
            {
                ErrorOr<BookingModel> authResult = await _mediator.Send(new BookingSingleReadQuery(idFlow));
                return authResult.Match(authResult => CustomOkResponse(_mapper.Map<BookingModel>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
            }
        }

        [HttpGet("/manage/{idBooking?}")]
        [ProducesResponseType(typeof(CustomResponse<BookingModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetManageBookings([FromRoute] int idFlow)
        {
            if (idFlow == 0)
            {
                ErrorOr<IEnumerable<BookingModel>> authResult = await _mediator.Send(new BookingMultipleReadQuery());
                return authResult.Match(authResult => CustomOkResponse(_mapper.Map<IEnumerable<BookingModel>>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
            }
            else
            {
                ErrorOr<BookingModel> authResult = await _mediator.Send(new BookingSingleReadQuery(idFlow));
                return authResult.Match(authResult => CustomOkResponse(_mapper.Map<BookingModel>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
            }
        }

        [HttpGet("history/{idBooking}")]
        [ProducesResponseType(typeof(CustomResponse<BookingHistoryModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHistory([FromRoute] int idBooking)
        {
            ErrorOr<IEnumerable<BookingHistoryModel>> authResult = await _mediator.Send(new BookingHistorySingleReadQuery(idBooking));
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<IEnumerable<BookingHistoryModel>>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
        }

        [HttpGet("types")]
        [ProducesResponseType(typeof(CustomResponse<BookingTypeModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotificationsTypes()
        {
            ErrorOr<IEnumerable<BookingTypeModel>> authResult = await _mediator.Send(new BookingTypeMultipleReadQuery());
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<IEnumerable<BookingTypeModel>>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
        }

        [HttpGet("status")]
        [ProducesResponseType(typeof(CustomResponse<NotificationWayModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNotificationsWays()
        {
            ErrorOr<IEnumerable<NotificationWayModel>> authResult = await _mediator.Send(new NotificationWayMultipleReadQuery());
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<IEnumerable<NotificationWayModel>>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResponse<BookingModel>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateBooking([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] BookingCreateCommand request)
        {
            ErrorOr<BookingModel> authResult = await _mediator.Send(request);
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<BookingModel>(authResult), "Registro creado correctamente"), errors => Problem(errors));
        }

        [HttpPut("aprove")]
        [ProducesResponseType(typeof(CustomResponse<BookingModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AproveBooking([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] BookingFullUpdateCommand request)
        {
            ErrorOr<BookingModel> authResult = await _mediator.Send(request);
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<BookingModel>(authResult), "Registro creado correctamente"), errors => Problem(errors));
        }

        [HttpPut("decline")]
        [ProducesResponseType(typeof(CustomResponse<BookingModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeclineBooking([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] BookingFullUpdateCommand request)
        {
            ErrorOr<BookingModel> authResult = await _mediator.Send(request);
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<BookingModel>(authResult), "Registro creado correctamente"), errors => Problem(errors));
        }

        [HttpPut]
        [ProducesResponseType(typeof(CustomResponse<BookingModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBooking([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] BookingFullUpdateCommand request)
        {
            ErrorOr<BookingModel> authResult = await _mediator.Send(request);
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<BookingModel>(authResult), "Registro creado correctamente"), errors => Problem(errors));
        }
    }
}
