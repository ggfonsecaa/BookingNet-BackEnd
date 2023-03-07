using BookingNet.Application.Contracts;

using Microsoft.AspNetCore.Mvc;

namespace BookingNet.Api.Helpers
{
    public class CustomDataResult<T> : IActionResult
    {

        private readonly CustomResponse<T> _result;

        public CustomDataResult(CustomResponse<T> result)
        {
            _result = result;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_result)
            {
                StatusCode = 200
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}