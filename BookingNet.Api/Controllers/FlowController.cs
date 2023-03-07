using AutoMapper;

using BookingNet.Application.Contracts;
using BookingNet.Application.Models.FlowsModels;
using BookingNet.Application.Services.FlowServices.Commands.Create;
using BookingNet.Application.Services.FlowServices.Commands.Delete;
using BookingNet.Application.Services.FlowServices.Commands.Update.Full;
using BookingNet.Application.Services.FlowServices.Queries.Read.Multiple;
using BookingNet.Application.Services.FlowServices.Queries.Read.Single;

using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [Route("api/v{version:apiVersion}/flows")]
    public class FlowController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FlowController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{idFlow?}")]
        [ProducesResponseType(typeof(CustomResponse<FlowModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroups([FromRoute] int idFlow)
        {
            if (idFlow == 0)
            {
                ErrorOr<IEnumerable<FlowModel>> authResult = await _mediator.Send(new FlowMultipleReadQuery());
                return authResult.Match(authResult => CustomOkResponse(_mapper.Map<IEnumerable<FlowModel>>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
            }
            else
            {
                ErrorOr<FlowModel> authResult = await _mediator.Send(new FlowSingleReadQuery(idFlow));
                return authResult.Match(authResult => CustomOkResponse(_mapper.Map<FlowModel>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResponse<FlowModel>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] FlowCreateCommand request)
        {
            ErrorOr<FlowModel> authResult = await _mediator.Send(request);
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<FlowModel>(authResult), "Registro creado correctamente"), errors => Problem(errors));
        }

        [HttpPut]
        [ProducesResponseType(typeof(CustomResponse<FlowModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] FlowFullUpdateCommand request)
        {
            ErrorOr<FlowModel> authResult = await _mediator.Send(request);
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<FlowModel>(authResult), "Registro actualizado correctamente"), errors => Problem(errors));
        }

        //[HttpPatch("{idFlow}")]
        //[ProducesResponseType(typeof(CustomResponse<FlowModel>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> UpdateUser([FromRoute, Required] int idFlow, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required]
        //JsonPatchDocument<FlowModel> request)
        //{
        //    ErrorOr<FlowModel> authResult = await _mediator.Send(new GroupPartialUpdateCommand(idFlow, request));
        //    return authResult.Match(authResult => CustomOkResponse(_mapper.Map<FlowModel>(authResult), "Registro actualizado correctamente"), errors => Problem(errors));
        //}

        [HttpDelete("{idFlow}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser([FromRoute, Required] int idFlow)
        {
            ErrorOr<FlowModel> authResult = await _mediator.Send(new FlowDeleteCommand(idFlow));
            return authResult.Match(authResult => CustomOkResponse<FlowModel>(null, "Registro eliminado correctamente"), errors => Problem(errors));
        }
    }
}
