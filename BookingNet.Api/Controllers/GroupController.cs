using AutoMapper;

using BookingNet.Application.Contracts;
using BookingNet.Application.Models.GroupModels;
using BookingNet.Application.Services.GroupServices.Commands.Create;
using BookingNet.Application.Services.GroupServices.Commands.Delete;
using BookingNet.Application.Services.GroupServices.Commands.Update.Full;
using BookingNet.Application.Services.GroupServices.Commands.Update.Partial;
using BookingNet.Application.Services.GroupServices.Queries.Read.Multiple;
using BookingNet.Application.Services.GroupServices.Queries.Read.Single;

using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace BookingNet.Api.Controllers
{
    [EnableCors]
    [Authorize]
    [ApiController]
    //[ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60, NoStore = false)]
    [ApiVersion("1.0")]
    //[ValidateAntiForgeryToken]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/groups")]
    public class GroupController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GroupController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{idGroup?}")]
        [ProducesResponseType(typeof(CustomResponse<GroupModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroups([FromRoute] int idGroup)
        {
            if (idGroup == 0)
            {
                ErrorOr<IEnumerable<GroupModel>> authResult = await _mediator.Send(new GroupMultipleReadQuery());
                return authResult.Match(authResult => CustomOkResponse(_mapper.Map<IEnumerable<GroupModel>>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
            }
            else
            {
                ErrorOr<GroupModel> authResult = await _mediator.Send(new GroupSingleReadQuery(idGroup));
                return authResult.Match(authResult => CustomOkResponse(_mapper.Map<GroupModel>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResponse<GroupModel>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] GroupCreateCommand request)
        {
            ErrorOr<GroupModel> authResult = await _mediator.Send(request);
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<GroupModel>(authResult), "Registro creado correctamente"), errors => Problem(errors));
        }

        [HttpPut]
        [ProducesResponseType(typeof(CustomResponse<GroupModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] GroupFullUpdateCommand request)
        {
            ErrorOr<GroupModel> authResult = await _mediator.Send(request);
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<GroupModel>(authResult), "Registro actualizado correctamente"), errors => Problem(errors));
        }

        [HttpPatch("{idGroup}")]
        [ProducesResponseType(typeof(CustomResponse<GroupModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser([FromRoute, Required] int idGroup, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required]
        JsonPatchDocument<GroupModel> request)
        {
            ErrorOr<GroupModel> authResult = await _mediator.Send(new GroupPartialUpdateCommand(idGroup, request));
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<GroupModel>(authResult), "Registro actualizado correctamente"), errors => Problem(errors));
        }

        [HttpDelete("{idGroup}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser([FromRoute, Required] int idGroup)
        {
            ErrorOr<GroupModel> authResult = await _mediator.Send(new GroupDeleteCommand(idGroup));
            return authResult.Match(authResult => CustomOkResponse<GroupModel>(null, "Registro eliminado correctamente"), errors => Problem(errors));
        }


        [HttpGet("{idGroup}/role/{idRole?}")]
        public async Task<IActionResult> GetGroupsByUser([FromRoute, Required] int idGroup, [FromRoute, Optional] int? idRole)
        {
            //ErrorOr<IEnumerable<GroupModel>> authResult = await _mediator.Send(new UserGroupsMultipleReadQuery(idGroup));
            //return authResult.Match(authResult => CustomOkResponse(_mapper.Map<IEnumerable<GroupModel>>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
            return Ok();
        }

        [HttpPost("{idGroup}/role")]
        public async Task<IActionResult> AddGroupToUser([FromRoute, Required] int idGroup,
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] GroupModel request)
        {
            //ErrorOr<GroupModel> authResult = await _mediator.Send(request);
            //return authResult.Match(authResult => CustomOkResponse(_mapper.Map<GroupModel>(authResult), "Registro creado correctamente"), errors => Problem(errors));
            return Ok();
        }

        [HttpPut("{idGroup}/role")]
        public async Task<IActionResult> UpdateGroupToUser([FromRoute, Required] int idGroup,
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] GroupModel request)
        {
            //ErrorOr<GroupModel> authResult = await _mediator.Send(request);
            //return authResult.Match(authResult => CustomOkResponse(_mapper.Map<GroupModel>(authResult), "Registro actualizado correctamente"), errors => Problem(errors));
            return Ok();
        }

        [HttpPatch("{idGroup}/role/{idRole}")]
        public async Task<IActionResult> UpdateGroupToUser([FromRoute, Required] int idGroup, [FromRoute, Required] int idRole,
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] JsonPatchDocument<GroupModel> request)
        {
            //ErrorOr<GroupModel> authResult = await _mediator.Send(new UserPartialUpdateCommand(idGroup, request));
            //return authResult.Match(authResult => CustomOkResponse(_mapper.Map<GroupModel>(authResult), "Registro actualizado correctamente"), errors => Problem(errors));
            return Ok();
        }

        [HttpDelete("{idGroup}/role/{idRole}")]
        public async Task<IActionResult> DeleteGroupToUser([FromRoute, Required] int idGroup, [FromRoute, Required] int idRole)
        {
            //ErrorOr<GroupModel> authResult = await _mediator.Send(new UserDeleteCommand(idGroup));
            //return authResult.Match(authResult => CustomOkResponse<GroupModel>(null, "Registro eliminado correctamente"), errors => Problem(errors));
            return Ok();
        }
    }
}