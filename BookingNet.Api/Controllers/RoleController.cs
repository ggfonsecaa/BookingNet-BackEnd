using AutoMapper;

using BookingNet.Application.Contracts;
using BookingNet.Application.Models.RoleModels;
using BookingNet.Application.Services.RoleServices.Commands.Create;
using BookingNet.Application.Services.RoleServices.Commands.Delete;
using BookingNet.Application.Services.RoleServices.Commands.Update.Full;
using BookingNet.Application.Services.RoleServices.Queries.Read.Multiple;
using BookingNet.Application.Services.RoleServices.Queries.Read.Single;

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
    [Route("api/v{version:apiVersion}/roles")]
    public class RoleController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RoleController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{idRole?}")]
        [ProducesResponseType(typeof(CustomResponse<RoleModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroups([FromRoute] int idRole)
        {
            if (idRole == 0)
            {
                ErrorOr<IEnumerable<RoleModel>> authResult = await _mediator.Send(new RoleMultipleReadQuery());
                return authResult.Match(authResult => CustomOkResponse(_mapper.Map<IEnumerable<RoleModel>>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
            }
            else
            {
                ErrorOr<RoleModel> authResult = await _mediator.Send(new RoleSingleReadQuery(idRole));
                return authResult.Match(authResult => CustomOkResponse(_mapper.Map<RoleModel>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomResponse<RoleModel>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] RoleCreateCommand request)
        {
            ErrorOr<RoleModel> authResult = await _mediator.Send(request);
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<RoleModel>(authResult), "Registro creado correctamente"), errors => Problem(errors));
        }

        [HttpPut]
        [ProducesResponseType(typeof(CustomResponse<RoleModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] RoleFullUpdateCommand request)
        {
            ErrorOr<RoleModel> authResult = await _mediator.Send(request);
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<RoleModel>(authResult), "Registro actualizado correctamente"), errors => Problem(errors));
        }

        //[HttpPatch("{idRole}")]
        //[ProducesResponseType(typeof(CustomResponse<RoleModel>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> UpdateUser([FromRoute, Required] int idRole, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required]
        //JsonPatchDocument<RoleModel> request)
        //{
        //    ErrorOr<RoleModel> authResult = await _mediator.Send(new GroupPartialUpdateCommand(idRole, request));
        //    return authResult.Match(authResult => CustomOkResponse(_mapper.Map<RoleModel>(authResult), "Registro actualizado correctamente"), errors => Problem(errors));
        //}

        [HttpDelete("{idRole}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser([FromRoute, Required] int idRole)
        {
            ErrorOr<RoleModel> authResult = await _mediator.Send(new RoleDeleteCommand(idRole));
            return authResult.Match(authResult => CustomOkResponse<RoleModel>(null, "Registro eliminado correctamente"), errors => Problem(errors));
        }

    }
}
