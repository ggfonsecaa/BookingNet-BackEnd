using AutoMapper;

using BookingNet.Application.Contracts;
using BookingNet.Application.Models.GroupModels;
using BookingNet.Application.Models.UserGroupModel;
using BookingNet.Application.Models.UserModels;
using BookingNet.Application.Services.UserGroupServices.Commands.Create;
using BookingNet.Application.Services.UserGroupServices.Queries.Read.Multiple;
using BookingNet.Application.Services.UserServices.Commands.Create;
using BookingNet.Application.Services.UserServices.Commands.Delete;
using BookingNet.Application.Services.UserServices.Commands.Update.Full;
using BookingNet.Application.Services.UserServices.Commands.Update.Partial;
using BookingNet.Application.Services.UserServices.Queries.Read.Multiple;
using BookingNet.Application.Services.UserServices.Queries.Read.Single;
using BookingNet.Application.Services.UserServices.Queries.Search;

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
    [Route("api/v{version:apiVersion}/users")]
    public class UserController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{idUser?}")]
        [ProducesResponseType(typeof(CustomResponse<UserModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers([FromRoute] int idUser)
        {
            if (idUser == 0)
            {
                ErrorOr<IEnumerable<UserModel>> authResult = await _mediator.Send(new UserMultipleReadQuery());
                return authResult.Match(authResult => CustomOkResponse(_mapper.Map<IEnumerable<UserModel>>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
            }
            else 
            { 
                ErrorOr<UserModel> authResult = await _mediator.Send(new UserSingleReadQuery(idUser));
                return authResult.Match(authResult => CustomOkResponse(_mapper.Map<UserModel>(authResult), "Datos cargados correctamente"), errors => Problem(errors)); 
            }
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(CustomResponse<IEnumerable<UserModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersByQuery([FromQuery] UserSearchQuery? query)
        {
            ErrorOr<IEnumerable<UserModel>> authResult = await _mediator.Send(query);
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<IEnumerable<UserModel>>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
        }

        /*[HttpGet]
        [ProducesResponseType(typeof(CustomResponse<IEnumerable<UserModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers()
        {
            ErrorOr<IEnumerable<UserModel>> authResult = await _mediator.Send(new UserMultipleReadQuery());
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<IEnumerable<UserModel>>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
        }*/

        [HttpPost]
        [ProducesResponseType(typeof(CustomResponse<UserModel>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] UserCreateCommand request)
        {
            ErrorOr<UserModel> authResult = await _mediator.Send(request);
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<UserModel>(authResult), "Registro creado correctamente"), errors => Problem(errors));
        }

        [HttpPut]
        [ProducesResponseType(typeof(CustomResponse<UserModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] UserFullUpdateCommand request)
        {
            ErrorOr<UserModel> authResult = await _mediator.Send(request);
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<UserModel>(authResult), "Registro actualizado correctamente"), errors => Problem(errors));
        }

        [HttpPatch("{idUser}")]
        [ProducesResponseType(typeof(CustomResponse<UserModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser([FromRoute, Required] int idUser, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] 
        JsonPatchDocument<UserModel> request)
        {
            ErrorOr<UserModel> authResult = await _mediator.Send(new UserPartialUpdateCommand(idUser, request));
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<UserModel>(authResult), "Registro actualizado correctamente"), errors => Problem(errors));
        }

        [HttpDelete("{idUser}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser([FromRoute, Required] int idUser)
        {
            ErrorOr<UserModel> authResult = await _mediator.Send(new UserDeleteCommand(idUser));
            return authResult.Match(authResult => CustomOkResponse<UserModel>(null, "Registro eliminado correctamente"), errors => Problem(errors));
        }


        [HttpGet("{idUser}/groups/{idGroup?}")]
        public async Task<IActionResult> GetGroupsByUser([FromRoute, Required] int idUser, [FromRoute, Optional] int? idGroup)
        {
            ErrorOr<IEnumerable<UserModel>> authResult = await _mediator.Send(new UserGroupsMultipleReadQuery(idUser));
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<IEnumerable<UserModel>>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
        }

        [HttpPost("{idUser}/groups")]
        public async Task<IActionResult> AddGroupToUser([FromRoute, Required] int idUser,
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] UsersGroupsAsociationModel request)
        {
            ErrorOr<UsersGroupsAsociationModel> authResult = await _mediator.Send(new UsersGroupsCreateCommand(idUser, request));
            return authResult.Match(authResult => CustomOkResponse<UsersGroupsAsociationModel>(null, "Registro creado correctamente"), errors => Problem(errors));
        }

        [HttpPut("{idUser}/groups")]
        public async Task<IActionResult> UpdateGroupToUser([FromRoute, Required] int idUser,
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] GroupModel request)
        {
            //ErrorOr<UserModel> authResult = await _mediator.Send(request);
            //return authResult.Match(authResult => CustomOkResponse(_mapper.Map<UserModel>(authResult), "Registro actualizado correctamente"), errors => Problem(errors));
            return Ok();
        }

        [HttpPatch("{idUser}/groups/{idGroup}")]
        public async Task<IActionResult> UpdateGroupToUser([FromRoute, Required] int idUser, [FromRoute, Required] int idGroup,
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] JsonPatchDocument<GroupModel> request)
        {
            //ErrorOr<UserModel> authResult = await _mediator.Send(new UserPartialUpdateCommand(idUser, request));
            //return authResult.Match(authResult => CustomOkResponse(_mapper.Map<UserModel>(authResult), "Registro actualizado correctamente"), errors => Problem(errors));
            return Ok();
        }

        [HttpDelete("{idUser}/groups/{idGroup}")]
        public async Task<IActionResult> DeleteGroupToUser([FromRoute, Required] int idUser, [FromRoute, Required] int idGroup)
        {
            ErrorOr<UserModel> authResult = await _mediator.Send(new UserDeleteCommand(idUser));
            return authResult.Match(authResult => CustomOkResponse<UserModel>(null, "Registro eliminado correctamente"), errors => Problem(errors));
        }
    }
}