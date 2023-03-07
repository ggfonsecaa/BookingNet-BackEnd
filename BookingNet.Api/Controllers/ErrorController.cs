using BookingNet.Domain.Exceptions;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookingNet.Auth.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/e")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error() 
        {
            Exception? exception = HttpContext.Features?.Get<IExceptionHandlerFeature>()?.Error;

            var (statusCode, message) = exception switch
            {
                IServiceException serviceException => ((int) serviceException.StatusCode, serviceException.ErrorMessage),
                _ => (StatusCodes.Status500InternalServerError, "Ocurrió un error interno")
            };

            return CreateValidationProblem(statusCode, "Se ha producido un error durante el procesamiento de la solicitud" , message);
        }

        private IActionResult CreateValidationProblem(int statusCode, string tittle, string error)
        {
            var modelStateDictionary = new ModelStateDictionary();
            modelStateDictionary.AddModelError(
                    "exception",
                    error);

            return ValidationProblem(modelStateDictionary: modelStateDictionary, title: tittle, statusCode: statusCode);
        }
    }
}