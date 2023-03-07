using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookingNet.Api.Helpers
{
    public static class ValidationHelper
    {

        public static IActionResult CreateValidationProblem(this ControllerBase controller, IEnumerable<ValidationFailure> errors) 
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(
                    error.PropertyName,
                    error.ErrorMessage);
            }

            return controller.ValidationProblem(modelStateDictionary: modelStateDictionary, title: "Uno o más errores ocurrieron durante la validación");
        }

        public static IActionResult CreateProblem(this ControllerBase controller, string messageError)
        {
            return controller.Problem(title: "Ocurrió un error durante la validación", detail: messageError);
        }
    }
}