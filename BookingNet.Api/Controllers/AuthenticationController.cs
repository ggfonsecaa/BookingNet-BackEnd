using AutoMapper;

using BookingNet.Api.Controllers;
using BookingNet.Api.Helpers;
using BookingNet.Application.Contracts.AuthContracts;
using BookingNet.Application.Interfaces;
using BookingNet.Application.Models.AuthModels;
using BookingNet.Application.Models.BookingModels;
using BookingNet.Application.Services.AuthServices.Login;
using BookingNet.Application.Services.AuthServices.Register;
using BookingNet.Application.Services.BookingServices.Queries.Read.Multiple.BookingQuery;
using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using System.ComponentModel.DataAnnotations;

namespace BookinNet.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [EnableCors]
    //[Authorize]
    [ApiController]
    //[ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60, NoStore = false)]
    [ApiVersion("1.0")]
    //[ValidateAntiForgeryToken]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthenticationController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginService"></param>
        /// <param name="registerService"></param>
        public AuthenticationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Realiza el login a la aplicación.
        /// </summary>
        /// <param userEmail="correo@dominio.com"></param>
        /// <param passWord="Abcd12345*"></param>
        /// <returns>Un token de sesión válido para ingresar a la aplicación</returns>
        /// <remarks>
        /// Ejemplo:
        ///
        ///     {
        ///        "userEmail": "ejemplo@correo.com",
        ///        "passWord": "Asdf1234"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Un token de sesión válido para ingresar a la aplicación</response>
        /// <response code="400">Si el usuario o la contraseña no son correctos, o si hay un error de validación de datos</response>
        /// <response code="500">Si ocurre algún error interno del servidor</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] LoginQuery request)
        {
            ErrorOr<LoginModel> authResult = await _mediator.Send(request);
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<LoginModel>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
        }

        /// <summary>
        /// Realiza el registro de un usuario en la aplicación.
        /// </summary>
        /// <param userName="usuario_app"></param>
        /// <param userEmail="correo@dominio.com"></param>
        /// <param passWord="Abcd12345*"></param>
        /// <returns>Un token de activación de la cuenta</returns>
        /// <remarks>
        /// Ejemplo:
        ///
        ///     {
        ///        "userName": "usuario_app",
        ///        "userEmail": "ejemplo@correo.com",
        ///        "passWord": "Asdf1234"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Se genera el token de activación y se envía al correo electrónico registrado</response>
        /// <response code="400">Si el usuario o el correo electrónico ya están registrados o si hay un error de validación de datos</response>
        /// <response code="500">Si ocurre algún error interno del servidor</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow), Required] RegisterCommand request)
        {
            ErrorOr<RegisterModel> authResult = await _mediator.Send(request);
            return authResult.Match(authResult => CustomOkResponse(_mapper.Map<RegisterModel>(authResult), "Datos cargados correctamente"), errors => Problem(errors));
        }
    }
}